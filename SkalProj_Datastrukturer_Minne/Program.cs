using System;
using System.Collections.Generic;

namespace SkalProj_Datastrukturer_Minne
{
    /**
     * Fråga 1. Stacken är en first in last out, vilket gör t.ex att den senast anropade metoden kommer vara den första som exkeveras. 
     *              Metod first { a = Metod second }
     *              Metod second { return Metod third }
     *              Metod third { return 1}
     *          Här kommer varje call på metoder läggas på stacken innan de körs, vilket så klart är bra eftersom first inte kan sättas föränn second har returnerat sitt värde,
     *          som den får av third. Om first kördes först hade a varit null, och det blir ju fel.
     * 
     *          Heapen är istället lagring av tillgänglig information som inte lagras lokalt/tillfälligt, utan behöver fortsätta finnas efter metoder kallats på. 
     *          
     *          
     * Fråga 2. En reference type håller en referens till ett objekt, den håller inget value i sig självt. Det är att likna en adress till ett hus, istället för själva huset.
     *              House house = new House();, så är house = 0x976326 eller liknande adress som pekar på vart huset och dess data faktiskt är i heapen.
     *          En value type håller däremot faktisk data, till exempel int i = 0, så är i = 0, och inte i = 0x091232.
     *          
     *          
     * Fråga 3. Den första metoden arbetar med value types, och eftersom x då innehåller värdet 3, så ändras aldrig värdet på 3. Det används bara för att sätta ett initialt värde på
     *          value typen y, som sedan ändras till 4. Eftersom y också är en value type och inte en referens, så ändras inte x.
     *          
     *          Den andra metoden jobbar istället med reference type. MyInt x är ett object som man använder en set-metod för att ändra data på. x.MyValue går i princip att läsas som
     *          0xaf6783.MyValue = 3; i det här fallet. När vi sedan sätter y = x, så blir y en reference type till samma referens som x, och y.MyValue blir även det 0xaf6783.MyValue = 4.
     *          Däreför returnerar x = 4.
     **/
    class Program
    {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {

            while (true)
            {
                Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4 ,5 ,0) of your choice"
                    + "\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n5. ReverseText"
                    + "\n0. Exit the application");
                char input = ' '; //Creates the character input to be used with the switch-case below.
                try
                {
                    input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    Console.Clear();
                    Console.WriteLine("Please enter some input!");
                }
                switch (input)
                {
                    case '1':
                        ExamineList();
                        break;
                    case '2':
                        ExamineQueue();
                        break;
                    case '3':
                        ExamineStack();
                        break;
                    case '4':
                        CheckParanthesis();
                        break;
                    case '5':
                        ReverseText();
                        break;
                    /*
                     * Extend the menu to include the recursive 
                     * and iterative exercises.
                     */
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }

        /// <summary>
        /// Examines the datastructure List
        /// </summary>
        static void ExamineList()
        {
            //Listan initieras med kapacitet av 4.
            List<string> list = new List<string>();
            bool run = true;
            Console.WriteLine("Type + to add to the list, - to remove. Text that follows will be what is added or removed. Type 0 to exit.");
            do
            {
                string input = Console.ReadLine();
                //Try-Catch för fallet att man försöker input en tom sträng.
                try 
                {
                    switch (input[0])
                    {
                        //Fråga 2 och 3. Om listan har samma Count som Capacity, och man försöker lägga till ett element, ökar listans Capacity med dubbla, alltså från 4 -> 8, 8 -> 16, osv.
                        //Fråga 4. Att öka i samma takt som element läggs till skulle vara väldigt ineffektivt då en ny array skapas varje gång listan ökar i storlek. 
                        case '+':
                            list.Add(input.Substring(1));
                            Console.WriteLine($"List count: {list.Count}, list capacity: {list.Capacity}.");
                            break;
                        //Fråga 5. Listan minskar aldrig i kapacitet, även om den töms från alla element och count når 0.
                        //Fråga 6. Man bör använda en array istället för lista när man känner till hur många element man som mest kommer ha, alltså när storleken ej behöver vara flexibel.
                        case '-':
                            list.Remove(input.Substring(1));
                            Console.WriteLine($"List count: {list.Count}, list capacity: {list.Capacity}.");
                            break;
                        case '0':
                            run = false;
                            break;
                        default:
                            Console.WriteLine("Input must start with either + or -, or type 0 to exit.");
                            break;
                    }
                } 
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input cannot be empty.");
                }
            } while (run);
        }

        /// <summary>
        /// Examines the datastructure Queue
        /// </summary>
        static void ExamineQueue()
        {
            Queue<string> queue = new Queue<string>();
            bool run = true;
            Console.WriteLine("Type + to add to the queue, text that follows will be what is added. Type - to remove next in queue. Type 0 to exit.");
            do
            {
                string input = Console.ReadLine();
                //Try-Catch för fallet att man försöker input en tom sträng.
                try
                {
                    switch (input[0])
                    {
                        //Lägger till element i kön. Eftersom det läggs till ett element i kön behöver vi inte hantera om Peek är null.
                        case '+':
                            string toEnqueue = input.Substring(1);
                            queue.Enqueue(toEnqueue);
                            Console.WriteLine($"Encueued {toEnqueue}. Queue count: {queue.Count}, next in line is {queue.Peek()}.");
                            break;
                        //Tar bort nästa element i kön. Vi testar utifall kön redan är tom, och kör enbart Peek om kön inte blir tom av borttagandet. 
                        case '-':
                            try
                            {
                                string removed = queue.Dequeue();
                                Console.Write($"Decued {removed}. Queue count: {queue.Count}");
                                if (queue.Count > 0) Console.WriteLine($", next in line is {queue.Peek()}.");
                                else Console.WriteLine(", line is currently empty.");
                            }
                            catch
                            {
                                Console.WriteLine("Queue is already empty.");
                            }                            
                            break;
                        case '0':
                            run = false;
                            break;
                        default:
                            Console.WriteLine("Input must start with either + or -, or type 0 to exit.");
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input cannot be empty.");
                }
            } while (run);
        }

        /// <summary>
        /// Examines the datastructure Stack
        /// </summary>
        static void ExamineStack()
        {
            Stack<string> stack = new Stack<string>();
            bool run = true;
            Console.WriteLine("Type + to push to the stack, text that follows will be what is pushed. Type - to pop item. Type 0 to exit.");
            do
            {
                string input = Console.ReadLine();
                //Try-Catch för fallet att man försöker input en tom sträng.
                try
                {
                    switch (input[0])
                    {
                        //Lägger till element i stacken. Eftersom det läggs till ett element i stacken behöver vi inte hantera om Peek är null.
                        case '+':
                            string toPush = input.Substring(1);
                            stack.Push(toPush);
                            Console.WriteLine($"Pushed {toPush}. Stack count: {stack.Count}, {stack.Peek()} is at the top.");
                            break;
                        //Poppar översta elementet i stacken. Vi testar utifall stacken redan är tom, och kör enbart Peek om stacken inte blir tom av poppen. 
                        case '-':
                            try
                            {
                                string popped = stack.Pop();
                                Console.Write($"Popped {popped}. Queue count: {stack.Count}");
                                if (stack.Count > 0) Console.WriteLine($", next to pop is {stack.Peek()}.");
                                else Console.WriteLine(", stack is currently empty.");
                            }
                            catch
                            {
                                Console.WriteLine("Stack is already empty.");
                            }
                            break;
                        case '0':
                            run = false;
                            break;
                        default:
                            Console.WriteLine("Input must start with either + or -, or type 0 to exit.");
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input cannot be empty.");
                }
            } while (run);

        }

        //Metod för att genom en stack skriva ut en valfri sträng baklänges.
        static void ReverseText()
        {
            Console.WriteLine("Input a line of text:");
            string toReverse;
            do
            {
                toReverse = Console.ReadLine();

                //Kollar om man skriver en rimlig sträng, en tom eller endbart mellanslag returnerar felmeddelande.
                //TODO: tvinga stängen att vara mer än 1 tecken.

                if (string.IsNullOrWhiteSpace(toReverse)) Console.WriteLine("Input cannot be empty.");
            } while (string.IsNullOrWhiteSpace(toReverse));

            Stack<char> reverseStack = new Stack<char> ();

            //Pushar alla chars i strängen till en stack.
            foreach (char c in toReverse)
            {
                reverseStack.Push(c);
            }
            //Kör en while-loop, så länge det finns element kvar i stacken så poppar vi dessa till en ny sträng. När stacken är tom skriver vi ut den nya strängen.

            string reversedString = "";
            while (reverseStack.TryPop(out char next))
            {
                reversedString += next;
            }

            Console.WriteLine(reversedString);
        }

        //Denna metod kollar att paranteser sluts korrekt med hjälp av stack. Eftersom en slutande parantes måste vara av samma karaktär som senaste öppnande parantes, fungerar stacks
        //FILO-princip perfekt. Ps. Min "på papper"-lösning är helt oläsbar, sorry för det. 😬
        static void CheckParanthesis()
        {
            Console.WriteLine("Input string to check:");
            string input;

            //Kollar om man skriver en rimlig sträng, en tom eller endbart mellanslag returnerar felmeddelande.
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) Console.WriteLine("Input cannot be empty.");
            } while (string.IsNullOrWhiteSpace(input));

            Stack<char> parenthesis = new Stack<char> ();
            //inTheClear bör vara true efter vi gått igenom strängen och allt ser bra ut. Om något är fel sätts den till false. Check sker efter hela strängen gåtts igenom.
            //TODO: Bryta forEach så fort inTheClear sätts till false. 
            bool inTheClear = true;

            //Går igenom varje char i strängen, men vi hanterar endast paranteser och ignorerar allt annat. 
            //TODO: Kanske en substräng istället med enbart de relevanta karaktärerna?
            foreach (char c in input)
            {
                //Man får alltid öppna en ny parantes, så där pushar vi enbart till stacken.
                if (c == '(' || c == '{' || c == '[')
                {
                    parenthesis.Push(c);
                }
                //När vi stänger en parantes kollar vi att den senast öppnade parantesen stämmer överens. Exempel: om ')' är nuvarande char, måste pop resultera i '('.
                if (c == ')' || c == '}' || c == ']')
                {
                    if (parenthesis.TryPop(out char result)) //Kollar först om det finns en öppnande parantes, och i så fall sätt den som result.
                    {
                        switch (c) //Switch för att kolla att motsvarande parantes stämmer.
                        {
                            case ')':
                                if (result != '(') inTheClear = false;
                                break;
                            case '}':
                                if (result != '{') inTheClear = false;
                                break;
                            case ']':
                                if (result != '[') inTheClear = false;
                                break;
                        }
                    } else inTheClear = false;
                }
            }
            //En sista koll utifall en parantes aldrig slöts även om alla tidigare var korrekta.
            if (parenthesis.TryPeek(out _)) inTheClear = false;

            Console.WriteLine(inTheClear ? "No errors found.":"Errors found, please close parenthesis correctly.");
        }
    }
}

