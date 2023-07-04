using Models;

using static System.Windows.Forms.LinkLabel;

namespace EmployeesPairRanking
{
    public partial class frmCommonProjetcs : Form
    {
        public frmCommonProjetcs()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get file selected
            DialogResult result = this.dlgSelectFile.ShowDialog();
            if (result == DialogResult.OK) 
            {
                // clear the grid and info labels
                this.lblCommonProjects.Text = string.Empty;
                this.lblResult.Text = string.Empty;
                this.lblCommonProjects.Visible = true;
                this.dgEmployyesPairRanking.DataSource = null;
                dgEmployyesPairRanking.Columns.Clear();

                this.lblSelectedFile.Text = $"Selected file: {dlgSelectFile.FileName}";
                this.lblSelectedFile.Visible = true;

                // import data
                List<EmployeeWorkData> empData;
                var importErrors = DataProcessor.FileImporter.Import(dlgSelectFile.FileName, out empData);
                if (importErrors.Count == 0)
                {
                    List<EmployeePairData> topPairData;
                    var finalResult = DataProcessor.PairDataProcessor.GetTopPairData(empData, out topPairData);

                    // show result
                    this.lblResult.Text = $"Pair ({finalResult.EmployeeID1}, {finalResult.EmployeeID2}) has worked together for {finalResult.DaysWorked} days.";
                    this.lblResult.Visible = true;

                    // setup the grid and show it
                    // create columns 
                    dgEmployyesPairRanking.Columns.Add("ID1", "Employee ID #1");
                    dgEmployyesPairRanking.Columns.Add("ID2", "Employee ID #2");
                    dgEmployyesPairRanking.Columns.Add("PROJECT_ID", "Project ID #1");
                    dgEmployyesPairRanking.Columns.Add("DAYS_WORKED", "Days worked");
                    // increase the width
                    dgEmployyesPairRanking.Columns["ID1"].Width = 120;
                    dgEmployyesPairRanking.Columns["ID2"].Width = 120;
                    // map columns to the data fileds
                    dgEmployyesPairRanking.Columns["ID1"].DataPropertyName = "EmployeeID1";
                    dgEmployyesPairRanking.Columns["ID2"].DataPropertyName = "EmployeeID2";
                    dgEmployyesPairRanking.Columns["PROJECT_ID"].DataPropertyName = "ProjectID";
                    dgEmployyesPairRanking.Columns["DAYS_WORKED"].DataPropertyName = "DaysWorked";


                    this.dgEmployyesPairRanking.DataSource = topPairData.ToArray();
                    this.dgEmployyesPairRanking.Visible = true;

                    this.lblCommonProjects.Visible = true;
                }
                else
                {
                    // import failed
                    this.lblCommonProjects.Text = "Import data has failed. See errors below";
                    this.lblCommonProjects.Visible = true;

                    // list all errors
                    dgEmployyesPairRanking.Columns.Add("ERROR", "ERROR");
                    var data = importErrors.ToArray();
                    for (int i = 0; i < data.Length; i++)
                    {
                        dgEmployyesPairRanking.Rows.Add(new object[] { data[i] });
                    };

                    var column = dgEmployyesPairRanking.Columns[0];
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    this.dgEmployyesPairRanking.Visible = true;
                }
            }

        }
    }
}