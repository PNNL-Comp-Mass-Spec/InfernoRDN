using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmPatterns : Form
    {
        private int nPatterns = 3;
        private int nDatasets = 10;
        private Dictionary<string, List<double>> mhtPatterns;
        private PointPairList list;
        private List<string> marrDatasets = new List<string>();
        private readonly clsPatternSearchPar mclsPatternPar;

        public frmPatterns(clsPatternSearchPar mclsPatterns)
        {
            InitializeComponent();
            mclsPatternPar = mclsPatterns;
        }

        private void GeneratePatterns()
        {
            // Random number generator for random shapes
            var RandomNum = new Random();

            mhtPatterns = new Dictionary<string, List<double>>();

            for (var k = 0; k < nPatterns; k++)
            {
                var pattern = new List<double>();
                pattern.Clear();
                for (var m = 0; m < nDatasets; m++)
                {
                    pattern.Add(RandomNum.NextDouble());
                }
                mhtPatterns.Add((k + 1).ToString(), pattern);
            }
        }


        // Call this method from the Form_Load method, passing your ZedGraphControl
        public void CreateChart(ZedGraphControl zgc, PointPairList pplist)
        {
            var myMaster = zgc.MasterPane;

            myMaster.PaneList.Clear();

            // Set the masterpane title
            myMaster.Title.Text = "";
            myMaster.Title.IsVisible = false;

            // Fill the masterpane background with a color gradient
            myMaster.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Set the margins to 10 points
            myMaster.Margin.All = 10;

            // Initialize a color and symbol type rotator
            var rotator = new ColorSymbolRotator();

            // Create some new GraphPanes
            for (var j = 0; j < nPatterns; j++)
            {
                // Create a new graph - rect dimensions do not matter here, since it
                // will be resized by MasterPane.AutoPaneLayout()
                var myPane = new GraphPane(new Rectangle(10, 10, 10, 10),
                                           "Pattern: " + (j + 1).ToString(),
                                           "Dataset Number",
                                           "Intensity (normalized)");

                myPane.Title.FontSpec.Size = 12F;
                myPane.XAxis.Title.FontSpec.Size = 10F;
                myPane.YAxis.Title.FontSpec.Size = 10F;
                myPane.YAxis.Scale.Min = -0.1;
                myPane.YAxis.Scale.Max = 1;
                // Make the XAxis start with the first label at -1
                myPane.XAxis.Scale.BaseTic = -1;

                // Hide the legend
                myPane.Legend.IsVisible = false;

                // Fill the GraphPane background with a color gradient
                myPane.Fill = new Fill(Color.White, Color.LightYellow, 45.0F);
                myPane.BaseDimension = 6.0F;

                // Data to plot
                list = new PointPairList();
                var vpattern = mhtPatterns[(j + 1).ToString()];

                for (var i = 0; i < vpattern.Count; i++)
                {
                    var x = (double)i + 1;
                    var y = vpattern[i];

                    list.Add(x, y);
                }

                // Add a curve to the Graph, use the next sequential color and symbol
                var myCurve = myPane.AddCurve("Type " + j,
                                              list, rotator.NextColor, rotator.NextSymbol);

                myCurve.Line.Width = 1.5F;
                myCurve.Symbol.Fill = new Fill(Color.White);
                myCurve.Symbol.Size = 5;

                // Fill the axis background with a gradient
                myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

                // Add the GraphPane to the MasterPane
                myMaster.Add(myPane);
            }

            using (var g = CreateGraphics())
            {
                // Tell ZedGraph to auto layout the new GraphPanes
                myMaster.SetLayout(g, PaneLayout.SquareRowPreferred);
            }

            zgc.AxisChange();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            mhtPatterns.Clear();
            nPatterns = zg1.MasterPane.PaneList.Count;

            for (var k = 0; k < nPatterns; k++)
            {
                var pattern = new List<double>();

                for (var m = 0; m < nDatasets; m++)
                {
                    pattern.Add(zg1.MasterPane.PaneList[k].CurveList[0].Points[m].Y);
                }
                mhtPatterns.Add((k + 1).ToString(), pattern);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            marrDatasets = mclsPatternPar.Datasets;
            this.PopulateListView = marrDatasets;
            this.DataSetName = mclsPatternPar.mstrDatasetName;

            nDatasets = marrDatasets.Count;
            GeneratePatterns();
            CreateChart(zg1, list);
            zg1.IsEnableVEdit = true;
        }

        public clsPatternSearchPar clsPatternPar
        {
            get
            {
                mclsPatternPar.mhtVectorPatterns = mhtPatterns;
                mclsPatternPar.nPatterns = nPatterns;

                return mclsPatternPar;
            }
        }

        public List<string> PopulateListView
        {
            set
            {
                marrDatasets = value;
                var lstVcolln = new ListViewItem[marrDatasets.Count];

                for (var i = 0; i < marrDatasets.Count; i++)
                {
                    var lstVItem = new ListViewItem((i + 1).ToString());
                    lstVItem.SubItems.Add(marrDatasets[i]);
                    lstVItem.Tag = i;
                    lstVcolln[i] = lstVItem;
                }
                mlstViewDataSets.Items.AddRange(lstVcolln);
            }
        }

        public int NumPatterns
        {
            set => nPatterns = value;
        }

        public string DataSetName
        {
            set => mlblDataName.Text = value;
        }
    }
}