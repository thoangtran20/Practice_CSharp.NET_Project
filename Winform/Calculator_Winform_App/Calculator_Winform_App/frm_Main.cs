namespace Calculator_Winform_App
{
    public partial class frm_Main : Form
    {
        private double result = 0;
        private string operationPerformed = "";
        private bool isOperationPerformed = false;
        public frm_Main()
        {
            InitializeComponent();
        }
        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (result == 0)
            {
                operationPerformed = button.Text;
                result = double.Parse(txtResult.Text);
                lblCurentOperation.Text = result + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {
                btnEqual.PerformClick();
                operationPerformed = button.Text;
                lblCurentOperation.Text += " " + operationPerformed;
                isOperationPerformed = true;
            }
        }

        private double currentResult = 0;

        private void btnEqual_Click(object sender, EventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    txtResult.Text = (result + double.Parse(txtResult.Text)).ToString();
                    break;
                case "-":
                    txtResult.Text = (result - double.Parse(txtResult.Text)).ToString();
                    break;
                case "*":
                    txtResult.Text = (result * double.Parse(txtResult.Text)).ToString();
                    break;
                case "/":
                    txtResult.Text = (result / double.Parse(txtResult.Text)).ToString();
                    break;
            }
            result = double.Parse(txtResult.Text);
            operationPerformed = "";
            lblCurentOperation.Text = "";

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            lblCurentOperation.Text = "";
            operationPerformed = "";
            result = 0;
        }

        private void button_click(object sender, EventArgs e)
        {
            if ((txtResult.Text == "0") || (isOperationPerformed))
            {
                txtResult.Clear();
            }
            isOperationPerformed = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!txtResult.Text.Contains("."))
                    txtResult.Text = txtResult.Text + button.Text;
            }
            else
            {
                txtResult.Text = txtResult.Text + button.Text;
            }
        }
    }
}