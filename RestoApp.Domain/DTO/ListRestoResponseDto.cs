﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Domain.DTO
{
    public class ListRestoResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<RestoResponseDto> Datas { get; set; }
    }
}
