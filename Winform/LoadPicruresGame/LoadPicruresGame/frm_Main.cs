namespace LoadPicturesGame
{
    public partial class frm_Main : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        string first;
        string second;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox pic1;
        PictureBox pic2;
        int totalTime = 30;
        int countDownTime;
        bool gameOver = false;

        public frm_Main()
        {
            InitializeComponent();
            LoadPictures();
        }

        private void LoadPictures()
        {
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;

            for (int i = 0; i < 12; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 50;
                newPic.Width = 50;
                newPic.BackColor = Color.BlueViolet;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);

                if (rows < 3)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos += 60;
                }
                if (rows == 3)
                {
                    leftPos = 20;
                    topPos += 60;
                    rows = 0;
                }
            }
            RestartGame();
        }

        private void NewPic_Click(object? sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (gameOver)
            {
                // don't register a click if the game is over
                return;
            }

            if (first == null)
            {
                pic1 = sender as PictureBox;
                if (pic1.Tag != null && pic1.Image == null)
                {
                    pic1.Image = Image.FromFile(@"E:\mooict-picture-matching-game-images\pics\" + (string)pic1.Tag + ".png");
                    first = (string)pic1.Tag;
                }
            }
            else if (second == null)
            {
                pic2 = sender as PictureBox;
                if (pic2.Tag != null && pic2.Image == null)
                {
                    pic2.Image = Image.FromFile(@"E:\mooict-picture-matching-game-images\pics\" + (string)pic2.Tag + ".png");
                    second = (string)pic2.Tag;
                }
            }
            else
            {
                CheckPictures(pic1, pic2);
            }
        }

        private void CheckPictures(PictureBox pic1, PictureBox pic2)
        {
            if (first == second)
            {
                pic1.Tag = null;
                pic2.Tag = null;
            }
            else
            {
                tries++;
                lblStatus.Text = "Mismatched " + tries + " times.";
            }

            first = null;
            second = null;

            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }

            // now lets check if all of the items have been solved

            if (pictures.All(o => o.Tag == pictures[0].Tag))
            {
                GameOver("Great Work, You Win!!!!");
            }
        }

        private void RestartGame()
        {
            // randomise the original list
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            // assign the random list to the original
            numbers = randomList;

            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString();
            }

            tries = 0;
            lblStatus.Text = "Mismatched: " + tries + " times. ";
            lblTimeLeft.Text = "Time Left: " + totalTime;
            gameOver = false;
            GameTimer.Start();
            countDownTime = totalTime;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            countDownTime--;

            lblTimeLeft.Text = "Time Left: " + countDownTime;

            if (countDownTime < 1)
            {
                GameOver("Times Up, You Lose");

                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile(@"E:\mooict-picture-matching-game-images\pics\" + (string)x.Tag + ".png");
                    }
                }
            }
        }

        private void GameOver(string message)
        {
            GameTimer.Stop();
            gameOver = true;
            MessageBox.Show(message + " Click Restart to Play Again!!!", "Thank for Playing");
        }
    }
}