﻿using System;
using System.Collections.Generic;

namespace syncinpos.Sessions.Dto
{
    public class ApplicationInfoDto
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Dictionary<string, bool> Features { get; set; }
        public DateTime OpenedDay { get; set; }
    }
}
