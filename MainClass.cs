using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using static System.Console;

namespace Lab7
{
    internal class MainClass
    {


         public static int[][] GenerateMatrix(int n, int m)
         {
             Random random = new Random();
             int[][] matrix = new int[n][];


             for (int i = 0; i < n; i++)
             {

                 matrix[i] = new int[m];
                 for (int j = 0; j < m; j++)
                 {
                     matrix[i][j] = random.Next(-100, 100);

                 }
             }

             return matrix;
         }
        
         public static void Main(string[] args)
         {
              int nRowsFirst = -1;
              int nColumnFirst = -1;
              int nRowsSecond = -2;
              int nColumnSecond = -2;
              WriteLine("Введіть кількість рядків та стовпців першої матриці через кому без пробілів ");
              string[] firstDimension = ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);
              WriteLine("Введіть кількість рядків та стовпців другої матриці через кому без пробілів ");
              string[] secondDimension = ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);
              try
              {
                   nRowsFirst = Convert.ToInt32(firstDimension[0]);
                   nColumnFirst = Convert.ToInt32(firstDimension[1]);
                   nRowsSecond = Convert.ToInt32(secondDimension[0]);
                   nColumnSecond = Convert.ToInt32(secondDimension[1]);
                if (nColumnFirst == nRowsSecond)
                {

                    int[][] firstMatrix = GenerateMatrix(nRowsFirst, nColumnFirst);
                    int[][] secondMatrix = GenerateMatrix(nRowsSecond, nColumnSecond);

            
                    WriteLine("Another result");

                    for (int i = 0; i < nRowsFirst; i++)
                    {

                        for (int j = 0; j < nColumnSecond; j++)
                        {
                            int sum = 0;
                            for (int k = 0; k < nColumnFirst; k++)
                            {
                                sum += firstMatrix[i][k] * secondMatrix[k][j];
                            }

                            WriteLine(sum);
                        }

                    }

                    var bufferBlock = new BufferBlock<int[][]>();


                    for (int i = 0; i < nRowsFirst; i++)
                    {
                        for (int k = 0; k < nColumnSecond; k++)
                        {
                           
                            int[][] data = new int[2][];
                            data[0] = firstMatrix[i];
                            
                            data[1] = new int[nRowsSecond];
                            for (int j = 0; j < nRowsSecond; j++)
                            {
                                    data[1][j] = secondMatrix[j][k];
                            }
                          
                            
                            bufferBlock.Post(data);
                        }
                    }


                    var actionBlock = new ActionBlock<int[][]>(n =>
                    {
                        int sum = 0;
                        WriteLine("IN BLOCK");
                       for(int i=0; i < nRowsSecond; i++)
                        {
                            
                            sum += n[0][i] * n[1][i];

                        }
                        WriteLine("SUM " + sum);
                    });


                    
                     for(int i=0; i <= nRowsFirst * nColumnSecond; i++)
                    {
                        actionBlock.Post(bufferBlock.Receive());
                    }



                }
                  else
                  {
                      WriteLine("Матриці неможливо перемножити оскільки кількість стовпців першої матриці не дорівнює кількості рядкам другої матриці");
                  }
              }
              catch(Exception e)
              {
                  WriteLine($"Увага допущені помилки при введені\n{e}\n");
              }

            
            
        }
       
    }

}
