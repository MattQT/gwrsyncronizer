using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using gwrsyncronizer.DataReader;

namespace UnitTestgwrsyncronizer.Test_DataReader
{
    [TestClass]
    public class Test_ReadFileData
    {
        #region Csv
        [TestMethod]
        public void Test_If_Csv_IsNot_Empty()
        {
            // Given
            string path = Directory.GetCurrentDirectory();
            var file = @"C:\Workspace\development\VisualStudio\gwrhousingsyncronizer\XUnitGwrHousingSyncronizer\Resources\SO.csv";

            // When
            ReadFileData rfd = new ReadFileData();
            var filevalues = rfd.ReadFileCsv(file);
            // Then
            Assert.IsNotNull(filevalues);
        }
        #endregion
        #region Pdf
        [TestMethod]
        public void Test_If_Pdf_IsNot_Empty()
        {
            // Given
            string path = Directory.GetCurrentDirectory();
            // var file = @"C:\Workspace\development\VisualStudio\gwrhousingsyncronizer\XUnitGwrHousingSyncronizer\Resources\GEB_PUBLIC191544631.pdf";
            // var file = @"C:\Workspace\development\VisualStudio\gwrhousingsyncronizer\XUnitGwrHousingSyncronizer\Resources\GEB_PUBLIC302034483.pdf";
            // var file = @"C:\Workspace\development\VisualStudio\gwrhousingsyncronizer\XUnitGwrHousingSyncronizer\Resources\GEB_PUBLIC160000643.pdf";
            //var file = @"C:\Workspace\development\VisualStudio\gwrhousingsyncronizer\XUnitGwrHousingSyncronizer\Resources\GEB_PUBLIC1328932.pdf";
            //var file = @"C:\Workspace\development\gwrsyncronizer\UnitTestgwrsyncronizer\Resources\GEB_PUBLIC793635fr.pdf";
            //var file = @"C:\Workspace\development\gwrsyncronizer\UnitTestgwrsyncronizer\Resources\GEB_PUBLIC793635de.pdf";
            //var file = @"C:\Workspace\development\gwrsyncronizer\UnitTestgwrsyncronizer\Resources\GEB_PUBLIC819938fr.pdf";
            //var file = @"C:\Workspace\development\gwrsyncronizer\UnitTestgwrsyncronizer\Resources\GEB_PUBLIC759972it.pdf";
            //var file = @"C:\Workspace\development\gwrsyncronizer\UnitTestgwrsyncronizer\Resources\GEB_PUBLIC760571it.pdf";
            var file = @"C:\Workspace\Projects\2018_080_002_SEP\00_Datengrundlage\gwr_daten\SO_pdf\pdf\190009706.pdf";

            // When
            ReadFileData rfd = new ReadFileData();
            var filevalues = rfd.ReadFilePdf(file);

            // Then
            Assert.IsNotNull(filevalues);
        }
        #endregion
    }
}
