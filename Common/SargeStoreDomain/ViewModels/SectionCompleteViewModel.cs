﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SargeStoreDomain.ViewModels
{
    public class SectionCompleteViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }

        public int? CurrentParentSection { get; set; }

        public int? CurrentSectionId { get; set; }
    }
}
