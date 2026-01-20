using System;

namespace Ruv.WebSite.Utilidades.Controles.GridCustomPager {

    public partial class CustomPageChangeArgs : EventArgs {

        private int _currentPageNumber;
        public int CurrentPageNumber 
        {
            get { return _currentPageNumber; }
            set { _currentPageNumber = value; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        private int _currentPageSize;
        public int CurrentPageSize
        {
            get { return _currentPageSize; }
            set { _currentPageSize = value; }
        }
    }

}