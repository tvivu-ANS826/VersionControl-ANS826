﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excelq = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.Entity.Migrations.Model;
using System.Drawing.Text;
using System.Windows.Markup;

namespace Excel
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excelq.Application xlApp;
        Excelq.Workbook xlWB;
        Excelq.Worksheet xlSheet;


        public Form1()
        {
            InitializeComponent();
            LoadData();

        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();

        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excelq.Application();

                xlWB = xlApp.Workbooks.Add(Missing.Value);

                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;

            }
            catch (Exception ex)
            {

                string errMsg = string.Format("Error: {0}\nLine : {1}", ex.Message, ex.Source);

                MessageBox.Show(errMsg, "ERROR");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)",
            };

            for (int cells = 1; cells < length; cells++)
            {
                xlSheet.Cells[1, 1] = headers[0];

                object[,] values = new object[Flats.Count, headers.Length];

                int counter = 0;

                foreach (Flat f in Flats)
                {
                    values[counter, 0] = f.Code;
                    values[counter, 8] = "";
                    counter++;

                    xlSheet.get_Range(
                                GetCell(2, 1),
                                GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

                    GetCell string = "A9";
                }
            }

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);



        }


       

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }





    }
}
