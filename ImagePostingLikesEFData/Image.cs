﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePostingLikesEFData
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int Likes { get; set; }

        public DateTime Date { get; set; }

    }
}
