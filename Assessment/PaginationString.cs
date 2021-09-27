using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Assessment
{
    public class PaginationString : IPagination<string>
    {
        private readonly IEnumerable<string> data;
        private readonly IEnumerable<string> auxdata;
        private readonly int pageSize;
        private int currentPage;
        private string sort;
        

        public PaginationString(string source, int pageSize, IElementsProvider<string> provider, string option)
        {
            data = provider.ProcessData(source,option);
            auxdata = provider.ProcessData(source,option);
            currentPage = 0;
            this.pageSize = pageSize;
            sort = null;

        }
        public void FirstPage()
        {
            currentPage = 0;
        }

        public void GoToPage(int page)
        {   
            int auxCurrent = currentPage;
            currentPage = page-1;
            if(!GetVisibleItems().Any()){
                currentPage=auxCurrent;
            }
        }

        public void LastPage()
        {
            int dataLength = data.Count();
            int lastPage = dataLength/pageSize;
            currentPage = (int)lastPage - 1;
            
        }

        public void NextPage()
        {
            if(currentPage < 0)
                FirstPage();
            else{
                if(GetVisibleItems().Any())
                    currentPage+=1;
            }
        }

        public void PrevPage()
        {            
                currentPage-=1;
        }
        public void SortPage(string s){
            sort=s;
        }
        public IEnumerable<string> GetVisibleItems()
        {
            if(sort!=null){
                if(sort == "asc"){
                    return data.OrderBy(x => x).Skip(currentPage*pageSize).Take(pageSize);    
                }else{
                    return data.OrderBy(x => x).Reverse().Skip(currentPage*pageSize).Take(pageSize);
                }
            }else{
                return auxdata.Skip(currentPage*pageSize).Take(pageSize);    
            }
        }

        public int CurrentPage()
        {
            return currentPage;   
        }
        public int Pages()
        {
            int dataLength = data.Count();
            int lastPage = dataLength/pageSize;
            return (int)lastPage;
        }
    }
}