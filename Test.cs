namespace endpointManager
{
    public class Test
    {
        private LogicLayer _logic;
        private int unexpected_errors;
        private int sucessfuly;
        private int errors;
        public Test(LogicLayer logicLayer){
            this._logic = logicLayer;
            this.unexpected_errors = 0;
            this.sucessfuly = 0;
            this.errors = 0;
            LoadData();
            RunTests();
            PrintReport();
        }

        private void LoadData(){ // mocking data manually with insert method
            try{_logic.Insert("ALDSK", "NSX1P2W", 121, "DADASk1521", "Disconnected");}
            catch(System.Exception){this.errors++; this.unexpected_errors++;}
            try{_logic.Insert("DNADL", "NSX1P3W", 245, "52155135", "Connected");}
            catch(System.Exception){this.errors++; this.unexpected_errors++;}
            try{_logic.Insert("D656D", "NSX2P3W", 35, "15135.da", "Armed");}
            catch(System.Exception){this.errors++; this.unexpected_errors++;}
        }

        private void RunTests(){// execute 5 operations Insert, Edit, Delete, List, Search            
            Console.WriteLine("\n\n\n[START TESTS]\n----");
            try
            {                        
                // insert     
                testInsert();
                testInsertErrors();
                // select
                testSelect();
                testSelectErrors();
                // update
                testUpdate();
                testUpdateErrors();
                // delete
                testDelete();
                testDeleteErrors();
                // list
                testList();

            }
            catch (System.Exception)
            {
                this.unexpected_errors++;
            }
            Console.WriteLine("[FINISH TESTS]\n----");
        }

        private void testList(){
            try{
                Console.Write("[Expected == YES] - Should List?: ");
                this._logic.List();
                Console.Write("YES.\n");
                this.sucessfuly++;
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }

        private void testDelete(){
            try{
                Console.Write("[Expected == YES] - Should Delete?: ");
                this._logic.Delete("D656D");
                Console.Write("YES.\n");
                try{
                    this._logic.Search("D656D");
                }catch(EndpointNotFoundError){
                    this.sucessfuly++;
                }                
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }
        private void testDeleteErrors(){
            try{
                Console.Write("[Expected == NO ] - Should delete non registered Serial Number?: ");
                this._logic.Delete("ALDSKPOU");
                Console.Write("YES.\n");
                this.errors++;
            }catch (EndpointNotFoundError){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }

        private void testUpdate(){
            try{
                Console.Write("[Expected == YES] - Should Update?: ");
                this._logic.Edit("D656D","Connected");
                Console.Write("YES.\n");
                if ("Connected" == this._logic.Search("D656D").State)
                    this.sucessfuly++;
                else
                    this.errors++;
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }
        private void testUpdateErrors(){
            try{
                Console.Write("[Expected == NO ] - Should update non registered Serial Number?: ");
                this._logic.Edit("ALDSKPOU","Connected");
                Console.Write("YES.\n");
                this.errors++;
            }catch (EndpointNotFoundError){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }

            try{
                Console.Write("[Expected == NO ] - Should update invalid State?: ");
                this._logic.Edit("DNADL","OTHER");
                Console.Write("YES.\n");
                this.errors++;
            }catch (InvalidSwitchState){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }

        private void testSelect(){
            try{
                Console.Write("[Expected == YES] - Should select?: ");
                this._logic.Search("ALDSK");
                Console.Write("YES.\n");
                this.sucessfuly++;
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }
        private void testSelectErrors(){
            try{
                Console.Write("[Expected == NO ] - Should select non registered Serial Number?: ");
                this._logic.Search("ALDSKPOU");
                Console.Write("YES.\n");
                this.errors++;
            }catch (EndpointNotFoundError){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }

        private void testInsert(){
            try{
                Console.Write("[Expected == YES] - Should insert?: ");
                this._logic.Insert("ALDGK", "NSX1P2W", 121, "DADASk1521", "Disconnected");
                Console.Write("YES.\n");
                this.sucessfuly++;
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }

        private void testInsertErrors(){
            try{
                Console.Write("[Expected == NO ] - Should insert with invalid state?: ");
                _logic.Insert("656DA", "NSX3P4W", 454, "58441888", "Other"); 
                Console.Write("YES.\n");
                this.errors++;
            }catch (InvalidSwitchState){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
            
            try{
                Console.Write("[Expected == NO ] - Should insert invalid model?: ");
                _logic.Insert("DAAGL", "NSX1P3A", 245, "52155135", "Connected"); 
                Console.Write("YES.\n");
                this.errors++;
            }catch (InvalidModel){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
            
            try{
                Console.Write("[Expected == NO ] - Should insert Serial Number already registered?: ");
                _logic.Insert("DNADL", "NSX3P4W", 2985, "52155135", "Connected"); 
                Console.Write("YES.\n");
                this.errors++;
            }catch (SerialNumberAlreadyExists){ 
                Console.Write("NO.\n");
                this.sucessfuly++; 
            }catch(System.Exception e){
                Console.Write($"Unexpected error\n({e.ToString()}).\n");
                this.errors++;
                this.unexpected_errors++;
            }
        }


        private void PrintReport(){
            Console.WriteLine($"\n\n[REPORT]\n\nTests Passed: {this.sucessfuly}\nTests Failed: {this.errors}\nUnexpected errors: {this.unexpected_errors}\n");
            Console.Write("Press [Enter] to continue.");
            Console.ReadLine();
        }

    }
}
