using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CopilotUI.Controls
{
    public partial class UITextBox : UserControl
    {

        private int _width = 0;
        private int _left = 0;
        private int _labelWidth = 0;

        public UITextBox()
        {
            InitializeComponent();
            this.panel1.Width = this.label1.Width;
            this.textBox1.Text = "";
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = 28;//锁定高度为28;
        }

        #region 设计器属性
        [Category("Custome"), Browsable(true)]
        public string Caption
        {
            get => this.label1.Text;
            set
            {
                #region 改变前的属性,记录这些属性,当caption属性改变的时候.根据caption改变前后的差值dis,调整整个控件的宽度,X轴坐标以及label1所在panel的宽度
                _width = this.Size.Width;//整体控件的宽度
                _left = this.Location.X;//控件的X轴坐标
                _labelWidth = this.label1.Width;//文本值改变前的label和label所在panel的宽度;
                #endregion

                this.label1.Text = value;
                var dis = this.label1.Width - _labelWidth;//记录改变前后的差值(记录的是给label1.Text赋值后的label.Width的差值)
                #region 改变后的属性
                this.Width = _width + dis;//根据差值, 调整整个控件的宽度
                this.Location = new Point(_left - dis, this.Location.Y);//根据差值 ,移动整个控件的X轴坐标
                this.panel1.Width = _labelWidth + dis; //根据差值,动态调整Panel的宽度,来拟合整个控件的的宽度变化
                #endregion
            }
        }

        private string _text;

        [Category("Custome"), Browsable(true), DefaultValue("")]
        public override string Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        #endregion

        [Category("Custome"), Browsable(true)]
        public new event EventHandler TextChanged;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(textBox1, e);
        }
    }
}
