﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.Client.Web.BlazorUI
{
    public class RGB
    {
        private double red;
        private double green;
        private double blue;

        public double Red
        {
            get => red; set
            {
                red = Math.Clamp(value, 0, 255);
            }
        }
        public double Green
        {
            get => green; set
            {
                green = Math.Clamp(value, 0, 255);
            }
        }
        public double Blue
        {
            get => blue;
            set
            {
                blue = Math.Clamp(value, 0, 255);
            }
        }

        public HSV toHSV()
        {
            double hue;
            double saturation;

            var red = Red / 255;
            var green = Green / 255;
            var blue = Blue / 255;

            double cMax = Math.Max(red, Math.Max(green, blue));
            double cMin = Math.Min(red, Math.Min(green, blue));
            double span = cMax - cMin;

            hue = red == cMax ? (green - blue) / span % 6
                : green == cMax ? (blue - red) / span + 2
                : (red - green) / span + 4;

            saturation = cMin == 0 ? 0 : span / cMax;

            return new HSV()
            {
                Hue = Math.Floor(hue * 60),
                Saturation = Math.Floor(saturation * 100),
                Value = Math.Floor(cMax * 100)
            };
        }

        public Hex ToHex()
        {
            var myColor = Color.FromArgb(Convert.ToInt32(Red), Convert.ToInt32(Green), Convert.ToInt32(Blue));
            return new Hex() { ColorCode = myColor.Name.Remove(0, 2) };
        }

        public string ToCss(double alpha = 1)
        {
            return $"rgba({Red}, {Green}, {Blue}, {alpha})";
        }
    }
}
