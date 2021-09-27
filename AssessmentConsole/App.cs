using System;
using System.Linq;
using Assessment;
using System.Collections.Generic;

namespace AssessmentConsole
{
    public class App
    {
        
        public bool ProcessOption(string option) 
        {
            if (option == "1")
            {
                StartPagination();
                return false;
            }
            return true;
        }

        private void StartPagination()
        {
            string option = GetOption(
                @"Pagination commands\n
                1. Source data
                0. Back
                ");
             if (option == "1")
            {
                ProcessPagination();
            }
        }

        private void ProcessPagination()
        {
            string option = GetOption(
                @"Type: \n
                1. Comma separated data(,)
                2. Pipe separated data(|)
                3. Space separated data( )
                0. Go Back
                ");
            if (option == "1" || option == "2" || option == "3") 
            {
                string data = GetOption("Source data");
                NavigateData(data, option);
            } 
        }

        private void NavigateData(string data, string option)
        {
            string pageSize = GetOption("Type the Page size");
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(data, int.Parse(pageSize), provider,option);
            DoNavigation(pagination);
        }


        private void showData(IEnumerable<string> data,int currentPage,string type=null){
            string r = "";
            foreach (var item in data)
            {
                r = r + item + ",";
            }
            Console.WriteLine("*_Pagina_"+(currentPage<0?0:(currentPage+1)).ToString()+"_");
            switch(type){
                case "next":
                        if(!data.Any())
                            Console.WriteLine("NO EXISTE DATOS");
                        else
                            Console.WriteLine(r);    
                break;
                case "prev":
                    if(currentPage<0)
                        Console.WriteLine("NO EXITE DATOS ANTERIORES");
                    else
                        Console.WriteLine(r);
                break;
                default:
                    Console.WriteLine(r);
                break;
            }
            Console.WriteLine('*');
        }

        private void DoNavigation(IPagination<string> pagination)
        {
            bool exit = false;
            while(!exit)
            {
                Console.WriteLine("Current Page:" + pagination);
                 string option = GetOption(
                @"Type: \n
                    1. First page
                    2. Next page
                    3. Previous page
                    4. Last page
                    5. Go to page
                    6. Pages
                    7. Sort Page
                    0. Go Back
                ");
                switch(option){
                    case "1":
                        pagination.FirstPage();
                        showData(pagination.GetVisibleItems(),pagination.CurrentPage());
                    break;
                    case "2":
                        pagination.NextPage();
                        showData(pagination.GetVisibleItems(),pagination.CurrentPage(),"next");
                    break;
                    case "3":
                        pagination.PrevPage();
                        showData(pagination.GetVisibleItems(),pagination.CurrentPage(),"prev");
                    break;
                    case "4":
                        pagination.LastPage();
                        showData(pagination.GetVisibleItems(),pagination.CurrentPage());
                    break;
                    case "5":
                        string page = GetOption("go page");
                        pagination.GoToPage(Int32.Parse(page));
                        showData(pagination.GetVisibleItems(),pagination.CurrentPage());
                    break;
                    case "6":
                        string pages = "";
                        for (int i = 0; i < pagination.Pages(); i++){
                            pages = pages + (i+1) + ',';
                        }
                        Console.WriteLine(pages);
                    break;
                    case "7":
                        string sort = GetOption(
                        @"Type: \n
                            1. Asc
                            2. Desc
                            3. Original
                        ");
                        if(sort == "1"){
                            pagination.SortPage("asc");
                        }
                        if(sort == "2"){
                            pagination.SortPage("desc");
                        }
                        if(sort == "3"){
                            pagination.SortPage(null);
                        }
                    break;
                    case "0":
                        exit = true;
                    break;
                }
            }
    
        }

        

        private string GetOption(string message)
        {
            Console.WriteLine(message);
            Console.Write("> ");
            return Console.ReadLine();
        }
    }
}