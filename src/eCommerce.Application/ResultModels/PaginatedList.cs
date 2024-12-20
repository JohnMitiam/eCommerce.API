﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ResultModels
{
    public class PaginatedList<T> : List<T>
    {
        private readonly int _currentPage;
        private readonly int _pageSize;
        private readonly int _totalCount;

        public PaginatedList(List<T> items, int totalCount, int page, int pageSize)
        {
            _totalCount = totalCount;
            _pageSize = pageSize;
            _currentPage = page;
            AddRange(items);
        }

        public int Page { get { return _currentPage; } }
        public int PageSize { get { return _pageSize; } }
        public int TotalCount { get { return _totalCount; } }
        public int TotalPages => (int)Math.Ceiling(_totalCount / (double)_pageSize);
        public bool HasPrevious => (_currentPage > 1);
        public bool HasNext => (_currentPage < TotalPages);
    }
}
