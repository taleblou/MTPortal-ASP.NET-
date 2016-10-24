using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Paya.Captcha
{
    public class CaptchaImage
    {
        // Fields
        private string _familyName;
        private int _height;
        private Bitmap _image;
        private readonly Random _random;
        private readonly string _text;
        private int _width;

        // Methods
        public CaptchaImage(string s, int width, int height)
        {
            _random = new Random();
            _text = s;
            SetDimensions(width, height);
            GenerateImage();
        }

        public CaptchaImage(string s, int width, int height, string familyName)
        {
            this._random = new Random();
            this._text = s;
            this.SetDimensions(width, height);
            this.SetFamilyName(familyName);
            this.GenerateImage();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._image.Dispose();
            }
        }

        ~CaptchaImage()
        {
            this.Dispose(false);
        }

        private void GenerateImage()
    {
        SizeF size;
        Font font;
        Bitmap bitmap = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb);
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(0, 0, this._width, this._height);
        HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
        g.FillRectangle(hatchBrush, rect);
        float fontSize = rect.Height + 1;
        do
        {
            fontSize--;
            font = new Font(this._familyName, fontSize, FontStyle.Bold);
            size = g.MeasureString(this._text, font);
        }
        while (size.Width > rect.Width);
        StringFormat initLocal0 = new StringFormat {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        StringFormat format = initLocal0;
        GraphicsPath path = new GraphicsPath();
        path.AddString(this._text, font.FontFamily, (int) font.Style, font.Size, rect, format);
        PointF[] points = new PointF[] { new PointF(((float) this._random.Next(rect.Width)) / 4f, ((float) this._random.Next(rect.Height)) / 4f), new PointF(rect.Width - (((float) this._random.Next(rect.Width)) / 4f), ((float) this._random.Next(rect.Height)) / 4f), new PointF(((float) this._random.Next(rect.Width)) / 4f, rect.Height - (((float) this._random.Next(rect.Height)) / 4f)), new PointF(rect.Width - (((float) this._random.Next(rect.Width)) / 4f), rect.Height - (((float) this._random.Next(rect.Height)) / 4f)) };
        Matrix matrix = new Matrix();
        matrix.Translate(0f, 0f);
        path.Warp(points, rect, matrix, WarpMode.Perspective, 0f);
        hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
        g.FillPath(hatchBrush, path);
        int m = Math.Max(rect.Width, rect.Height);
        for (int i = 0; i < ((int) (((float) (rect.Width * rect.Height)) / 30f)); i++)
        {
            int x = this._random.Next(rect.Width);
            int y = this._random.Next(rect.Height);
            int w = this._random.Next(m / 50);
            int h = this._random.Next(m / 50);
            g.FillEllipse(hatchBrush, x, y, w, h);
        }
        font.Dispose();
        hatchBrush.Dispose();
        g.Dispose();
        this._image = bitmap;
    }

        private void SetDimensions(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
            }
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
            }
            this._width = width;
            this._height = height;
        }

        private void SetFamilyName(string familyName)
        {
            try
            {
                Font font = new Font(this._familyName, 12f);
                this._familyName = familyName;
                font.Dispose();
            }
            catch (Exception)
            {
                this._familyName = FontFamily.GenericSerif.Name;
            }
        }

        // Properties
        public int Height
        {
            get
            {
                return this._height;
            }
        }

        public Bitmap Image
        {
            get
            {
                return this._image;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
        }

    }
}