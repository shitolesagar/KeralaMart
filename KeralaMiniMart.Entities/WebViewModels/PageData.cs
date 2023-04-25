using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class PagingData
    {
        public PagingData(int totalRecordCount, int pageSize = 20, int pageIndex = 1)
        {
            UpToRecord = pageIndex * pageSize;
            FromRecord = UpToRecord - pageSize + 1;
            UpToRecord = totalRecordCount < UpToRecord ? totalRecordCount : UpToRecord;
            IsFirst = pageIndex == 1;
            IsLast = pageIndex == (int)Math.Ceiling(totalRecordCount / (decimal)pageSize);
            CurrentIndex = pageIndex;
            TotalCount = totalRecordCount;
        }

        public int UpToRecord { get; set; }
        public int FromRecord { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
    }
}

