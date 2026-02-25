        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable dt = ImportExcelToDataTable(openFileDialog.FileName);
                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        private DataTable ImportExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Handle both .xls and .xlsx
                if (Path.GetExtension(filePath).ToLower() == ".xls")
                    workbook = new HSSFWorkbook(file);
                else
                    workbook = new XSSFWorkbook(file);

                ISheet sheet = workbook.GetSheetAt(0); // Get first sheet
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                // 1. Setup Header (First Row)
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    // Use the cell value as column name, or "Column n" if empty
                    string columnName = cell?.ToString() ?? $"Column {j}";
                    dt.Columns.Add(columnName);
                }

                // 2. Setup Data (Start from index 1 to skip header)
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; // Skip empty rows

                    DataRow dataRow = dt.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }
