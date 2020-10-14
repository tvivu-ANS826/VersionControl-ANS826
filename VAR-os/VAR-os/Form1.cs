﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAR_os.Entities;

namespace VAR_os
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;

        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        List<decimal> nyereségekRendezve;

        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;

            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;

            PortfolioItem p = new PortfolioItem();
            p.Index = "OTP";
            p.Volume = 10;
            Portfolio.Add(p);


            /* int elemszám = Portfolio.Count();

             decimal részvényekSzáma = (from x in Portfolio select x.Volume).Sum();
             MessageBox.Show(string.Format("Részvények száma: {0}", részvényekSzáma));

             DateTime minDátum = (from x in Ticks select x.TradingDay).Min();

             DateTime maxDátum = (from x in Ticks select x.TradingDay).Max();

             int elteltNapokSzáma = (maxDátum - minDátum).Days;

             DateTime optMinDátum = (from x in Ticks where x.Index == "OTP" select x.TradingDay).Min();

             var kapcsolt =
                 from
                     x in Ticks
                          join
               y in Portfolio
                     on x.Index equals y.Index
                 select new
                 {
                     Index = x.Index,
                     Date = x.TradingDay,
                     Value = x.Price,
                     Volume = y.Volume
                 };
             dataGridView1.DataSource = kapcsolt.ToList(); */

            List<decimal> Nyereségek = new List<decimal>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }

            nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                        .ToList();
            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());



        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Comma Separeted Value (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            DialogResult eredmény = sfd.ShowDialog();

            if (eredmény != DialogResult.OK) return;

            StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8);

            sw.WriteLine("Időszak;Nyereség");

            int count = 1;

            foreach (var s in nyereségekRendezve)
            {
             
                sw.Write(count.ToString());
                count++;
                sw.Write(";");
                sw.Write(s.ToString());

                
                sw.WriteLine(); 
            }

            sw.Close();


            



        }
    }


}

