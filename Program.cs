using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize a string variable for storing dimension from user input
            string dim;
            do
            {
                Console.WriteLine("Enter two even numbers between 2 and 100 for Layout dimensions N and M");
                dim = Console.ReadLine();
                if(dim.Trim().Split(' ').Count()==2)
                {
                    foreach (string s in dim.Split(' '))
                    {
                        if (!s.All(char.IsNumber) || int.Parse(s) <= 0 || int.Parse(s) > 100 || int.Parse(s)%2!=0)
                        {
                            dim="";
                        }

                    }
                }

            }
            while (dim.Trim().Split(' ').Count() != 2);

            //Initialize integer variables to store N and M dimensions of the Brick Layout
            int n = int.Parse(dim.Split(' ')[0]);
            int m = int.Parse(dim.Split(' ')[1]);

            //Initialize a string variable to store brick values from user input
            string input = "";

            for (int i = 0; i < n; i++)
            {
                string temp = "";
                temp = Console.ReadLine();
                if(i<n-1)
                {
                    temp += ',';
                }
                input += temp;
            }

            //Initialize a two-dimensional array to store the layout from the brick value input
            int[,] Layout1;

            if (ValidateInput(input,n,m))
            {
                Layout1 = CreateLFoundation(input, n, m);  
            }
            else
            {
                Console.WriteLine("-1");
                Console.ReadKey();
                return; 
            }
            int[,] Layout2=AddLayout(Layout1);

            Console.WriteLine();
            Console.WriteLine("Layout1");
            DisplayLayout(Layout1);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Layout2");
            DisplayLayout(Layout2);

  
            Console.ReadKey();

        }





        //Method for validating if input can create a valid layout
        static bool ValidateInput(string s, int n, int m)
        {
            for(int i = 0;i<n;i++)
            {
                //initialize string for storing row elements
                string[] row = s.Trim(' ').Split(',')[i].Split(' ');
                if (row.Count() != m)
                {
                    Console.WriteLine("Trying to insert incorect amout of elements in row {0}",i+1);
                    return false;
                }
                for(int j = 0; j < row.Count(); j++)
                {
                    string el = row[j];
                    if(!el.All(char.IsNumber))
                    {
                        Console.WriteLine("Element E({0},{1}) is not a number", i + 1, j+1);
                        return false;
                    }
                    if (int.Parse(el) > (n*m/2) || int.Parse(el) <= 0)
                    {
                        Console.WriteLine("Element E({0},{1}) is not a valid number (1 - {2})", i + 1, j + 1, (n * m / 2));
                        return false;
                    }
                }  
            }
            //Create a layout from input to test for valid brick value position
            int[,] Layout = CreateLFoundation(s, n, m);

            //Initialize int variable for storing sum of all elements
            int sum = 0;
            foreach(int i in Layout)
            {
                sum += i;
            }

            if(sum != SumElements(n*m/2))
            {
                Console.WriteLine("Bricks are not of length 2");
                return false;
            }

            //Initialize queue for checking vertical matches in the array
            Queue<int> q = new Queue<int>();

            for(int i = 0; i<n; i++)
            {
                for (int j = 0; j < m; j++) 
                {

                    if (i == 0)
                    {
                        if (j == m - 1)
                        {
                            q.Enqueue(j);
                            continue;
                        }
                        if (j!=m-1 && Layout[i,j]!= Layout[i,j+1])
                        {
                            q.Enqueue(j);

                            continue;
                        }
                        else if (j != m)
                        {
                            j++;
                            continue;
                        }
                    }
                    if (j == m - 1&&Layout[i,j]!=Layout[i-1,j])
                    {
                        q.Enqueue(j);
                    }
                    if (q.Count() > 0 && q.Peek()==j)
                    {
                        q.Dequeue();
                        if(Layout[i,j]!= Layout[i-1,j])
                        {
                            Console.WriteLine("Some brick values do not form a 1x2 brick");
                            return false;
                        }
                    }
                    else if(j != m - 1 && Layout[i, j] != Layout[i, j + 1])
                    {

                        q.Enqueue(j);
                        continue;
                    }
                    else if (j != m)
                    {
                        j++;
                        continue;
                    }
                }
            }

            return true;
        }

        //Method for summing all the values in the layout to check brick length.
        static int SumElements(int x)
        {
            if (x > 1)
            {
                return x * 2 + SumElements(x - 1);
            }
            else
            {
                return 2;
            }
        }


        //Method for creating the base layout from user input
        static int[,] CreateLFoundation(string s,int n, int m)
        {
            //Initiialize a two-dimensional array to store the values of the Bricks
            int[,] Layout = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                //initialize string for storing row elements
                string[] row = s.Split(',')[i].Split(' ');
                for (int j = 0; j < m; j++)
                {
                    Layout[i,j] = int.Parse(row[j]);
                }
            }
            return Layout;
        }





        //Method for creating a new Layout based on a provided layout
        static int[,] AddLayout(int[,] layout1)
        {
            //Initialize integer for storing dimension of array
            int n = layout1.GetLength(0);
            int m = layout1.GetLength(1);

            //Initialize a a two-dumensional array to return the new layout
            int[,] layout2= new int[n, m];

            //initialize an integer variable for incrementing brick value
            int x = 1;
            for (int i = 0; i < n;i=i+2)
            {
                for(int j =0; j< m;j=j+2)
                {

                    if(layout1[i,j] == layout1[i,j+1] || layout1[i+1,j] == layout1[i+1,j+1])
                    {
                        layout2[i, j] = x;
                        layout2[i+1, j] = x;
                        layout2[i, j+1] = x + 1;
                        layout2[i+1, j+1] = x + 1;
                        x = x + 2;
                    }

                    else
                    {
                        layout2[i, j] = x;
                        layout2[i + 1, j] = x + 1;
                        layout2[i, j + 1] = x;
                        layout2[i + 1, j + 1] = x + 1;
                        x = x + 2;
                    }
                }
            }
            return layout2;
        }


        //Method for displaying layout to the console
        static void DisplayLayout(int[,] layout)
        {
            //Initialize queue for checking vertical matches in the array
            Queue<int> q = new Queue<int>();

            //Initialize integer for storing dimension of array
            int n = layout.GetLength(0);
            int m = layout.GetLength(1);

            //Initialize a string to print horizontal symbols for separating bricks
            string h = "";

            for (int i = 0; i < n; i++)
            {
                h = "|";
                Console.WriteLine();
                Console.Write("|");

                for (int j = 0; j < m; j++)
                {
                    if (i == 0)
                    {
                        if (j < m-1)
                        {
                            if (layout[i, j] == layout[i, j + 1])
                            {
                                Console.Write(" ");
                                if (layout[i, j] < 10)
                                {
                                    Console.Write(" ");
                                }
                                Console.Write(layout[i, j]);
                                Console.Write("   ");

                                if (layout[i, j] < 10)
                                {
                                    Console.Write(" ");
                                }
                                Console.Write(layout[i, j + 1]);
                                Console.Write(" |");
                                h += "---------|";
                                j++;
                                continue;
                            }
                            else
                            {
                                Console.Write(" ");
                                if (layout[i, j] < 10)
                                {
                                    Console.Write(" ");
                                }
                                Console.Write(layout[i, j]);
                                Console.Write(" |");
                                h += "    |";
                                q.Enqueue(j);
                                continue;
                            }
                        }
                        else if(j==m-1)
                        {
                            Console.Write(" ");
                            if (layout[i, j] < 10)
                            {
                                Console.Write(" ");
                            }
                            Console.Write(layout[i, j]);
                            Console.Write(" |");
                            h += "    |";
                            q.Enqueue(j);
                            continue;
                        }

                    }
                    else
                    {
                        if (j < m-1)
                        {
                            if (layout[i, j] == layout[i, j + 1])
                            {
                                Console.Write(" ");
                                if (layout[i, j] < 10)
                                {
                                    Console.Write(" ");    
                                }
                                Console.Write(layout[i, j + 1]);
                                Console.Write("   ");
                                
                                if (layout[i, j+1] < 10)
                                {
                                    Console.Write(" ");
                                    
                                }
                                Console.Write(layout[i, j + 1]);
                                Console.Write(" |");
                                h += "---------|";
                                j++;
                                continue;
                            }
                            else
                            {
                                if (q.Count() > 0)
                                {
                                    if (q.Peek() == j)
                                    {
                                        Console.Write(" ");
                                        if (layout[i, j] < 10)
                                        {
                                            Console.Write(" ");
                                        }
                                        Console.Write(layout[i, j]);
                                        Console.Write(" |");
                                        h += "----|";
                                        q.Dequeue();
                                        continue;
                                    }
                                    else
                                    {
                                        Console.Write(" ");
                                        if (layout[i, j] < 10)
                                        {
                                            Console.Write(" ");
                                        }
                                        Console.Write(layout[i, j]);
                                        Console.Write(" |");
                                        h += "    |";
                                        q.Enqueue(j);
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.Write(" ");
                                    if (layout[i, j] < 10)
                                    {
                                        Console.Write(" ");
                                    }
                                    Console.Write(layout[i, j]);
                                    Console.Write(" |");
                                    h += "    |";
                                    q.Enqueue(j);
                                    continue;
                                }
                                
                            }

                        }
                        else
                        {
                            if (q.Peek() == j)
                            {
                                Console.Write(" ");
                                if (layout[i, j] < 10)
                                {
                                    Console.Write(" ");
                                }
                                Console.Write(layout[i, j]);
                                Console.Write(" |");
                                h += "----|";
                                q.Dequeue();
                                continue;
                            }
                            else
                            {
                                Console.Write(" ");
                                if (layout[i, j] < 10)
                                {
                                    Console.Write(" ");
                                }
                                Console.Write(layout[i, j]);
                                Console.Write(" |");
                                h += "    |";
                                q.Enqueue(j);
                                continue;
                            }
                        }
                    }
                }
                Console.WriteLine();
                if(i!=n-1)
                Console.Write(h);
              
            }


        }

    }
}
