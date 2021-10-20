using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int CurrentLocationY = 0;
        public int CurrentLocationX = 0;
        public string QueryChosen = "";
        public string QueryChosenText = "";
        public string WordChosenText = "";
        public String SearchText = "";
        public String OffsetQuery = "0";
        public String FetchNext = "0";
        public string Layout = "";
        public string Filterid = "";
        public string fileid = "";
        public string fileText = "";
        public string WebmLocation = @"D:\SSIS_Projects\4ch_Viewer\Temporary_Files_For_App\";
        public Form1()
        {
            InitializeComponent();
            TbSearch.KeyDown += new KeyEventHandler(tb_KeyDown);
            FetchNext = TbFetch.Text.ToString();
        }
        private DataTable QueryMainTable()
        {
            string SearchQueryTopDuplicates = @"
;WITH T1 AS (
			SELECT 
			CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
			,A.[post_message]
			,D.[thread_subject] AS [thread_name]
			,B.[file_name]
			,A.[date_loaded] AS [insert_date]
			,A.[board_short] as [boards]
			,B.[md5] AS [file_md5]
			,E.[ile]
			,A.[thread_id] AS [id_thread]
			,A.[file_id] AS [id_file]
			,A.[post_id] AS [id_post]
			,ROW_NUMBER() OVER (PARTITION BY B.[md5] ORDER BY A.[date_loaded] DESC) AS RN
			FROM [4ch].[dbo].[post] A   JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
										LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
										JOIN	(
												SELECT 
												[md5]
												,COUNT(*) AS ile
												FROM [4ch].[dbo].[file]
												GROUP BY 
                                                [md5] 
                                                HAVING 
                                                COUNT(*) > 5
												) E ON B.[md5] = E.[md5]
			WHERE
            CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END IS NOT NULL
			)
SELECT
[file_body]
,[post_message]
,[thread_name]
,[file_name]
,[insert_date]
,[boards]
,[id_thread]
,[id_file]
,[id_post]
,[ile]
FROM T1
WHERE RN = 1
ORDER BY 
T1.[ile] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string TopDuplicatesToday = @"
;WITH T1 AS (
			SELECT 
			CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
			,A.[post_message]
			,D.[thread_subject] AS [thread_name]
			,B.[file_name]
			,A.[date_loaded] AS [insert_date]
			,A.[board_short] AS [boards]
			,E.[ile]
            ,A.[thread_id] AS [id_thread]
            ,A.[file_id] AS [id_file]
            ,A.[post_id] AS [id_post]
			,ROW_NUMBER() OVER (PARTITION BY B.[md5] ORDER BY A.[date_loaded] DESC) AS RN
			FROM [4ch].[dbo].[post] A	JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
										LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
										JOIN	(
												SELECT 
												a.[md5]
												,COUNT(*) AS ile
												FROM [4ch].[dbo].[file] a JOIN [4ch].[dbo].[post] b ON	a.[file_id] = b.[file_id]
                                                                                                        AND b.[date_loaded] >= DATEADD(DAY,-1,getdate())
												GROUP BY 
                                                a.[md5] 
                                                HAVING COUNT(*) > 5
												) E ON B.[md5] = E.[md5]
			WHERE
            CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END IS NOT NULL
			)
SELECT
[file_body]
,[post_message]
,[thread_name]
,[file_name]
,[insert_date]
,[boards]
,[id_thread]
,[id_file]
,[id_post]
,[ile]
FROM T1
WHERE 
RN = 1
ORDER BY 
T1.[ile] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string NewestWebms = @"
SELECT 
B.[thumbnail] AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] AS [boards]
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A	JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
WHERE 
B.[thumbnail] IS NOT NULL
AND RIGHT(B.[file_name],5) = '.webm'
ORDER BY 
A.[date_loaded] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string SearchQueryNewestPictures = @"
SELECT 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] AS [boards]
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A	JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
WHERE 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END IS NOT NULL
AND RIGHT(B.[file_name],5) <> '.webm'
ORDER BY 
A.[date_loaded] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string SearchQueryNewestPricturesWebms = @"
SELECT 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] AS [boards]
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A	JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
WHERE 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END IS NOT NULL
ORDER BY 
A.[date_loaded] desc
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string SearchQueryNewestAll = @"
SELECT 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] as [boards]
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A	LEFT JOIN [4ch].[dbo].[file] B ON	A.[file_id] = B.[file_id]
                                                                AND B.[file_body] IS NOT NULL
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
ORDER BY 
A.[date_loaded] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string SearchStandardQuery = @"
SELECT 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] as [boards]
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A   JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
WHERE 
(
B.[file_name] LIKE '%" + SearchText + @"%'
OR [thread_subject] LIKE '%" + SearchText + @"%'
OR [post_message] LIKE '%" + SearchText + @"%'
)
ORDER BY 
A.[date_loaded] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string SearchQueryThread = @"
SELECT 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] as [boards]
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A   LEFT JOIN [4ch].[dbo].[file] B ON   A.[file_id] = B.[file_id]
                                                                AND B.[file_body] is not null
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
WHERE 
A.[thread_id] = " + Filterid + @"
ORDER BY 
A.[post_id] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
";
            string SearchQueryFromWords = @"
SELECT 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END AS [file_body]
,A.[post_message]
,D.[thread_subject] AS [thread_name]
,B.[file_name]
,A.[date_loaded] AS [insert_date]
,A.[board_short] AS boards
,A.[thread_id] AS [id_thread]
,A.[file_id] AS [id_file]
,A.[post_id] AS [id_post]
FROM [4ch].[dbo].[post] A	JOIN [4ch].[dbo].[file] B ON A.[file_id] = B.[file_id]
							LEFT JOIN [4ch].[dbo].[thread] D ON A.[thread_id] = D.[thread_id]
WHERE 
CASE WHEN B.[thumbnail] IS NOT NULL THEN B.[thumbnail] ELSE B.[file_body] END IS NOT NULL
AND (
	B.[file_name] LIKE '%" + WordChosenText + @"%'
	OR D.[thread_subject] LIKE '%" + WordChosenText + @"%'
	OR A.[post_message] LIKE '%" + WordChosenText + @"%'
	)
ORDER BY 
A.[date_loaded] DESC
OFFSET " + OffsetQuery + @" ROWS
FETCH NEXT " + FetchNext + @" ROWS ONLY
                                            ";
            string QueryUsedWords = @"
SELECT 
TOP 2000 
[word] 
FROM [dbo].[word]
ORDER BY 
[counter] DESC
";
            if (QueryChosen == "SearchStandardQuery")
            {
                QueryChosenText = SearchStandardQuery;
            }
            else if (QueryChosen == "SearchQueryTopDuplicates")
            {
                QueryChosenText = SearchQueryTopDuplicates;
            }
            else if (QueryChosen == "SearchQueryNewestPictures")
            {
                QueryChosenText = SearchQueryNewestPictures;
            }
            else if (QueryChosen == "SearchQueryNewestPricturesWebms")
            {
                QueryChosenText = SearchQueryNewestPricturesWebms;
            }
            else if (QueryChosen == "NewestWebms")
            {
                QueryChosenText = NewestWebms;
            }
            else if (QueryChosen == "TopDuplicatesToday")
            {
                QueryChosenText = TopDuplicatesToday;
            } 
            else if (QueryChosen == "SearchQueryNewestAll")
            {
                QueryChosenText = SearchQueryNewestAll;
            }
            else if (QueryChosen == "QueryUsedWords")
            {
                QueryChosenText = QueryUsedWords;
            }
            else if (QueryChosen == "SearchQueryFromWords")
            {
                QueryChosenText = SearchQueryFromWords;
            }
            else if (QueryChosen == "SearchQueryThread")
            {
                QueryChosenText = SearchQueryThread;
            }
            System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection();
            sqlconn.ConnectionString = "Persist Security Info=False;Integrated Security=SSPI;database=4ch;server=DESKTOP-5VIUC65;Connection Timeout=3600";
            SqlDataAdapter sqladapt = new System.Data.SqlClient.SqlDataAdapter(QueryChosenText, sqlconn);
            DataTable tab = new DataTable();
            sqladapt.SelectCommand.CommandTimeout = 180;
            sqladapt.Fill(tab); 
            return tab;
        }
        private void RefreshForm() 
        {
            if (TSCBFormLayout.SelectedItem != null)
            {
                Layout = TSCBFormLayout.SelectedItem.ToString();
            }
            if (Layout == "Standard" || Layout == "")
            {
                int IdWiersza = 0;
                DataTable TableFromQuery = QueryMainTable();
                foreach (DataRow dtRow in TableFromQuery.Rows)
                {
                    string postText = TableFromQuery.Rows[IdWiersza][1].ToString();
                    string threadText = TableFromQuery.Rows[IdWiersza][2].ToString();
                    string fileText = TableFromQuery.Rows[IdWiersza][3].ToString();
                    string boardsText = TableFromQuery.Rows[IdWiersza][5].ToString();
                    string threadid = TableFromQuery.Rows[IdWiersza][6].ToString();
                    string fileid = TableFromQuery.Rows[IdWiersza][7].ToString();

                    int formwidth = Int32.Parse(this.Width.ToString());
                    int formheight = Int32.Parse(this.Height.ToString());

                    if (TableFromQuery.Rows[IdWiersza][0] != DBNull.Value)
                    {
                        MemoryStream ms = new MemoryStream((byte[])TableFromQuery.Rows[IdWiersza][0]);
                        var picture = new PictureBox
                        {
                            Name = "pictureBox",
                            Location = new Point(0, CurrentLocationY),
                            Image = new Bitmap(ms),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                        };


                        int imageHeight = picture.Image.Size.Height;
                        int imageWidth = picture.Image.Size.Width;
                        decimal aspectRatio = Decimal.Parse(imageHeight.ToString()) / Decimal.Parse(imageWidth.ToString());
                        if (imageWidth > formwidth - 25)
                        {
                            int correctedWidth = formwidth - 25;
                            int correctedHeight = Decimal.ToInt32(Decimal.Parse(correctedWidth.ToString()) * aspectRatio);
                            imageHeight = correctedHeight;
                            imageWidth = correctedWidth;
                            if (correctedHeight > 990)
                            {
                                correctedHeight = 990;
                                correctedWidth = Decimal.ToInt32(Decimal.Parse(correctedHeight.ToString()) / aspectRatio);
                                imageHeight = correctedHeight;
                                imageWidth = correctedWidth;
                            }
                            picture.Width = imageWidth;
                            picture.Height = imageHeight;
                        }
                        else
                        {
                            int correctedHeight;
                            int correctedWidth;
                            if (imageHeight > 990)
                            {
                                correctedHeight = 990;
                                correctedWidth = Decimal.ToInt32(Decimal.Parse(correctedHeight.ToString()) / aspectRatio);
                                imageHeight = correctedHeight;
                                imageWidth = correctedWidth;
                            }
                            picture.Width = imageWidth;
                            picture.Height = imageHeight;
                        }
                        Pan.Controls.Add(picture);

                        ContextMenuStrip CMS = new ContextMenuStrip();
                        CMS.Items.Add("Open thread: " + threadid);
                        CMS.Items.Add("Open file: " + fileid);
                        picture.ContextMenuStrip = CMS;
                        CMS.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);

                        ToolTip ttip = new ToolTip();
                        ttip.SetToolTip(picture, boardsText + "\n" + fileText + "\n" + threadText + "\n" + postText);

                        CurrentLocationY = CurrentLocationY + picture.Height;
                    }
                    /*
                    if (TableFromQuery.Rows[IdWiersza][0] != DBNull.Value && fileText.Substring(fileText.Length - 4, 4) == "webm")
                    {
                        string queryString = @"EXEC master..xp_cmdshell 'BCP ""SELECT [file_body] FROM [4ch].[dbo].[file] WHERE [file_id] = " + fileid+@""" queryout """+ WebmLocation + fileText + @""" -S DESKTOP-5VIUC65 -T -f D:\SSIS_Projects\4ch_Viewer\Format_file\formatfile.fmt'";
                        string connectionString = "Persist Security Info=False;Integrated Security=SSPI;database=4ch;server=DESKTOP-5VIUC65";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(queryString, connection);
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                        }
                        GetThumbnail(WebmLocation + fileText, WebmLocation + fileText + ".jpg");
                        AxWMPLib.AxWindowsMediaPlayer myMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
                        Pan.Controls.Add(myMediaPlayer);
                        myMediaPlayer.settings.playCount = 99;
                        myMediaPlayer.settings.autoStart = false;
                        myMediaPlayer.URL = WebmLocation + fileText;
                        myMediaPlayer.Ctlenabled = true;
                        myMediaPlayer.close();
                        myMediaPlayer.Location = new Point(0, CurrentLocationY);
                        myMediaPlayer.Width = 500;
                        myMediaPlayer.Height = 500;
                        myMediaPlayer.uiMode = "Full";                        
                        CurrentLocationY = CurrentLocationY + 500;

                    }
                    */
                    IdWiersza++;
                    
                    var tekst = new RichTextBox
                    {
                        Location = new Point(0, CurrentLocationY),
                        Text = threadText,
                        Width = ((formwidth - 25) / 20) * 6,
                        Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold),
                    };
                    Pan.Controls.Add(tekst);
                    var tekst2 = new RichTextBox
                    {
                        Location = new Point(((formwidth - 25) / 20) * 6, CurrentLocationY),
                        Text = postText,
                        Width = ((formwidth - 25) / 20) * 8,
                        Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold),
                    };
                    Pan.Controls.Add(tekst2);
                    var tekst3 = new RichTextBox
                    {
                        Location = new Point(((formwidth - 25) / 20) * 14, CurrentLocationY),
                        Text = fileText,
                        Width = ((formwidth - 25) / 20) * 4,
                        Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold),
                    };
                    Pan.Controls.Add(tekst3);
                    var tekst4 = new RichTextBox
                    {
                        Location = new Point(((formwidth - 25) / 20) * 18, CurrentLocationY),
                        Text = boardsText,
                        Width = ((formwidth - 25) / 20) * 1,
                        Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold),
                    };
                    Pan.Controls.Add(tekst4);
                    CurrentLocationY = CurrentLocationY + tekst.Height;
                    
                }
                Pan.Focus();
            }
            if (Layout == "SmallGrid")
            {
                int IdWiersza = 0;
                int[,] array_locationsY;
                int current_column = 0;
                int current_row = 0;
                int number_of_columns = 5;
                int number_of_pictures = 50;
                int width_of_single_picture = 380;
                array_locationsY = new int[number_of_pictures, number_of_pictures];
                array_locationsY[current_row, current_column] = 0;
              
                DataTable TableFromQuery = QueryMainTable();
                foreach (DataRow dtRow in TableFromQuery.Rows)
                {
                    string postText = TableFromQuery.Rows[IdWiersza][1].ToString();
                    string threadText = TableFromQuery.Rows[IdWiersza][2].ToString();
                    fileText = TableFromQuery.Rows[IdWiersza][3].ToString();
                    string boardsText = TableFromQuery.Rows[IdWiersza][5].ToString();
                    string threadid = TableFromQuery.Rows[IdWiersza][6].ToString();
                    fileid = TableFromQuery.Rows[IdWiersza][7].ToString();
                    /*
                    if (TableFromQuery.Rows[IdWiersza][0] != DBNull.Value && fileText.Substring(fileText.Length - 4, 4) == "webm")
                    {
                        WMPLib.WindowsMediaPlayer Player;
                        Player = new WMPLib.WindowsMediaPlayer();
                        MemoryStream ms = new MemoryStream((byte[])TableFromQuery.Rows[IdWiersza][0]);
                        Player.currentMedia = ;
                        Player.controls.play();
                    }
                    */
                    if (TableFromQuery.Rows[IdWiersza][0] != DBNull.Value)
                    {
                        MemoryStream ms = new MemoryStream((byte[])TableFromQuery.Rows[IdWiersza][0]);

                        try
                        {
                            var picture = new PictureBox
                            {
                                Name = "pictureBox",
                                Location = new Point(CurrentLocationX, array_locationsY[current_row, current_column]),
                                Image = new Bitmap(ms),
                                SizeMode = PictureBoxSizeMode.StretchImage,

                            };
                            IdWiersza++;
                            int formwidth = Int32.Parse(this.Width.ToString());
                            int formheight = Int32.Parse(this.Height.ToString());
                            int imageHeight = picture.Image.Size.Height;
                            int imageWidth = picture.Image.Size.Width;
                            decimal aspectRatio = Decimal.Parse(imageHeight.ToString()) / Decimal.Parse(imageWidth.ToString());

                            picture.Width = width_of_single_picture;
                            picture.Height = Convert.ToInt32(Decimal.Parse(width_of_single_picture.ToString()) * aspectRatio);

                            Pan.Controls.Add(picture);

                            ContextMenuStrip CMS = new ContextMenuStrip();
                            CMS.Items.Add("Open thread: " + threadid);
                            CMS.Items.Add("Open file: " + fileid);
                            picture.ContextMenuStrip = CMS;
                            CMS.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);

                            ToolTip ttip = new ToolTip();
                            ttip.SetToolTip(picture, boardsText + "\n" + fileText + "\n" + threadText + "\n" + postText);

                            array_locationsY[current_row, current_column] = array_locationsY[current_row, current_column] + picture.Height;
                            current_column = current_column + 1;
                            CurrentLocationX = CurrentLocationX + width_of_single_picture;
                        }
                        catch
                        {
                            //do NOTHING
                        }

                    }
                    if (current_column % (number_of_columns) == 0)
                    {
                        current_column = 0;
                        CurrentLocationX = 0;
                    }
                    
                }
                Pan.Focus();
            }
        }
        private void ClearForm()
        {
            ArrayList list = new ArrayList(Pan.Controls);
            foreach (Control c in list)
            {
                if (c is PictureBox)
                {
                    ((PictureBox)c).Image = null;
                }
                if (c is RichTextBox)
                {
                    ((RichTextBox)c).Text = null;
                }
            }
            Pan.Controls.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            CurrentLocationY = 0;
            CurrentLocationX = 0;

            System.IO.DirectoryInfo di = new DirectoryInfo(WebmLocation);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            SearchText = TbSearch.Text.ToString();
            QueryChosen = "SearchStandardQuery";
            OffsetQuery = "0";
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }
        private void BNext_Click(object sender, EventArgs e)
        {
            OffsetQuery = TbEnd.Text.ToString();
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }
        private void ToolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (CBSpecial.SelectedItem.ToString() == "Top Duplicates")
            {
                QueryChosen = "SearchQueryTopDuplicates";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest Webms")
            {
                QueryChosen = "NewestWebms";
            }
            if (CBSpecial.SelectedItem.ToString() == "Top Duplicates Today")
            {
                QueryChosen = "TopDuplicatesToday";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest With Pictures")
            {
                QueryChosen = "SearchQueryNewestPictures";
                TbFetch.Text = "200";
                TSCBFormLayout.SelectedItem = "SmallGrid";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest With Prictures Or Webms")
            {
                QueryChosen = "SearchQueryNewestPricturesWebms";
                TbFetch.Text = "200";
                TSCBFormLayout.SelectedItem = "SmallGrid";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest All")
            {
                QueryChosen = "SearchQueryNewestAll";
            }
            OffsetQuery = "0";
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchText = TbSearch.Text.ToString();
                QueryChosen = "SearchStandardQuery";
                OffsetQuery = "0";
                FetchNext = TbFetch.Text.ToString();
                TbStart.Text = OffsetQuery;
                TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
                ClearForm();
                RefreshForm();
            }
        }
        private void CBMostUsedWords_Click(object sender, EventArgs e)
        {
            QueryChosen = "QueryUsedWords";
            int IdWiersza = 0;
            DataTable TableFromQuery = QueryMainTable();
            foreach (DataRow dtRow in TableFromQuery.Rows)
            {
                string WordText = TableFromQuery.Rows[IdWiersza][0].ToString();
                IdWiersza++;
            }

            System.Object[] ItemObject = new System.Object[IdWiersza];
            for (int i = 0; i <= IdWiersza-1; i++)
            {
                ItemObject[i] = TableFromQuery.Rows[i][0].ToString();
            }
            CBMostUsedWords.Items.AddRange(ItemObject);
        }
        private void BUseWords_Click(object sender, EventArgs e)
        {
            WordChosenText = CBMostUsedWords.SelectedItem.ToString();
            QueryChosen = "SearchQueryFromWords";
            OffsetQuery = "0";
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }
        private void BPrevious_Click(object sender, EventArgs e)
        {
            OffsetQuery = (Int32.Parse(TbStart.Text.ToString()) - Int32.Parse(TbFetch.Text.ToString())).ToString();
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }
        public static void GetThumbnail(string video, string thumbnail)
        {
            var cmd = "ffmpeg  -itsoffset -1  -i " + '"' + video + '"' + " -vcodec mjpeg -vframes 1 -an -f rawvideo " + '"' + thumbnail + '"';

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + cmd
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit(5000);

            //return LoadImage(thumbnail);
        }

        static Bitmap LoadImage(string path)
        {
            var ms = new MemoryStream(File.ReadAllBytes(path));
            return (Bitmap)Image.FromStream(ms);
        }

        void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if(item.Text.Substring(0,13) == "Open thread: ")
            {
                Filterid = item.Text.Substring(13, item.Text.Length - 13);
                QueryChosen = "SearchQueryThread";
                OffsetQuery = "0";
                TbFetch.Text = "20";
                TSCBFormLayout.SelectedItem = "Standard";
                FetchNext = TbFetch.Text.ToString();
                TbStart.Text = OffsetQuery;
                TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
                ClearForm();
                RefreshForm();
            }

            else if (item.Text.Substring(0,11) == "Open file: ")
            {
                Filterid = item.Text.Substring(11, item.Text.Length - 11);
                string queryString = @"EXEC master..xp_cmdshell 'BCP ""SELECT [file_body] FROM [4ch].[dbo].[file] WHERE [file_id] = " + Filterid + @""" queryout """ + WebmLocation + Filterid + @""" -S DESKTOP-5VIUC65 -T -f D:\SSIS_Projects\4ch_Viewer\Format_file\formatfile.fmt'";
                string connectionString = "Persist Security Info=False;Integrated Security=SSPI;database=4ch;server=DESKTOP-5VIUC65";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }

                System.Diagnostics.Process.Start("mpc-hc64.exe", WebmLocation + Filterid);
            }
            //item.get
            //Filterid = item()
            // your code here
        }

        private void CBSpecial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBSpecial.SelectedItem.ToString() == "Top Duplicates")
            {
                QueryChosen = "SearchQueryTopDuplicates";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest Webms")
            {
                QueryChosen = "NewestWebms";
            }
            if (CBSpecial.SelectedItem.ToString() == "Top Duplicates Today")
            {
                QueryChosen = "TopDuplicatesToday";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest With Pictures")
            {
                QueryChosen = "SearchQueryNewestPictures";
                TbFetch.Text = "200";
                TSCBFormLayout.SelectedItem = "SmallGrid";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest With Prictures Or Webms")
            {
                QueryChosen = "SearchQueryNewestPricturesWebms";
                TbFetch.Text = "200";
                TSCBFormLayout.SelectedItem = "SmallGrid";
            }
            if (CBSpecial.SelectedItem.ToString() == "Newest All")
            {
                QueryChosen = "SearchQueryNewestAll";
            }
            OffsetQuery = "0";
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }

        private void CBMostUsedWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            WordChosenText = CBMostUsedWords.SelectedItem.ToString();
            QueryChosen = "SearchQueryFromWords";
            OffsetQuery = "0";
            FetchNext = TbFetch.Text.ToString();
            TbStart.Text = OffsetQuery;
            TbEnd.Text = (Int32.Parse(OffsetQuery) + Int32.Parse(FetchNext)).ToString();
            ClearForm();
            RefreshForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}