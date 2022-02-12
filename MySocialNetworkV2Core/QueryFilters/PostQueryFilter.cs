﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.QueryFilters
{
    public class PostQueryFilter
    {
        const int maxPageSize = 50;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string Fields { get; set; }
    }
}
