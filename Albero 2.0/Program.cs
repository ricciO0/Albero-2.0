using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albero_2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {



            AlberoBinarioIntero sx;
            AlberoBinarioIntero dx;
            AlberoBinarioIntero p;
            AlberoBinarioIntero r = new AlberoBinarioIntero("+");

            sx = new AlberoBinarioIntero("*", new AlberoBinarioIntero(2 + ""), new AlberoBinarioIntero(6 + ""));

            p = new AlberoBinarioIntero("-", new AlberoBinarioIntero(5 + ""), sx);

            sx = p;
            dx = new AlberoBinarioIntero("/", new AlberoBinarioIntero(8 + ""), new AlberoBinarioIntero(2 + ""));

            r.aggiungiFiglioDx(dx);
            r.aggiungiFiglioSx(sx);



            Console.WriteLine(r.FormulaIterativa());


            Console.ReadKey();



        }



        public class AlberoBinarioIntero
        {
            private string val=" ";
            private AlberoBinarioIntero dx;
            private AlberoBinarioIntero sx;
            


            public AlberoBinarioIntero(string val, AlberoBinarioIntero sx, AlberoBinarioIntero dx)
            {
                this.val = val;
                this.sx = sx;
                this.dx = dx;

            }

            public AlberoBinarioIntero(string a)
            {
                this.val = a+"";

            }
            public string getval()
            {
                return val;
            }

            public void aggiungiFiglioSx(AlberoBinarioIntero a)
            {
                this.sx = a;
            }

            public void aggiungiFiglioDx(AlberoBinarioIntero a)
            {
                this.dx = a;
            }



            public override string ToString()
            {
                if (dx != null && sx != null)
                {
                    return this.val + "(" + sx + "," + dx + ")";

                }
                else if (sx == null && dx == null)
                {
                    return this.val + "";
                }
                else if (sx != null && dx == null)
                {
                    return this.val + "(" + sx + "," + 0 + ")";
                }
                else
                {
                    return this.val + "(" + 0 + "," + dx + ")";
                }
            }

            public float FormulaRicorsiva()
            {
                
                switch (this.val)
                {
                    case "+":
                       return this.sx.FormulaRicorsiva()+this.dx.FormulaRicorsiva();
                        break;
                    case "-":
                        return this.sx.FormulaRicorsiva() - this.dx.FormulaRicorsiva();

                        break;

                    case "*":
                        return this.sx.FormulaRicorsiva() * this.dx.FormulaRicorsiva();

                        break;

                    case "/":
                        return this.sx.FormulaRicorsiva() / this.dx.FormulaRicorsiva();

                        break;
                    default:
                        return Convert.ToInt32(this.val);
                        break;
                }
            }

            public double FormulaIterativa()
            {
                Queue<AlberoBinarioIntero> coda =this.stampaRicorsivaPosticipata(new Queue<AlberoBinarioIntero>());

                AlberoBinarioIntero a=null, b=null;
                do
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 2 && !coda.ElementAt(i).control())
                        {

                            int tmpo = Convert.ToInt32(coda.Peek().val);
                            coda.Peek().val = coda.ElementAt(1).val;            
                            coda.ElementAt(1).val = coda.ElementAt(2).val;     
                            coda.ElementAt(2).val = tmpo + "";
                           


                        }
                        else if (i == 0)
                        {
                            a = coda.ElementAt(i);
                        }
                        else if (i == 1)
                        {
                            b = coda.ElementAt(i);
                        }

                        if (a != null && b != null && coda.ElementAt(i).control())
                        {
                            float k = 0;


                            switch (coda.ElementAt(i).val)
                            {
                                case "+":
                                    k = Convert.ToInt32(a.val) + Convert.ToInt32(b.val);
                                    coda.ElementAt(i).val = k + "";
                                    break;

                                case "-":
                                    k = Convert.ToInt32(a.val) - Convert.ToInt32(b.val);
                                    coda.ElementAt(i).val = k + "";
                                    break;

                                case "*":
                                    k = Convert.ToInt32(a.val) * Convert.ToInt32(b.val);
                                    coda.ElementAt(i).val = k + "";
                                    break;

                                case "/":
                                    k = Convert.ToInt32(a.val) / Convert.ToInt32(b.val);
                                    coda.ElementAt(i).val = k + "";
                                    break;
                            }

                            coda.Dequeue();
                            coda.Dequeue();
                            a = null;
                            b = null;
                        }
                    }
                } while (coda.Count > 1);

                return Convert.ToDouble(coda.Peek().val);
            }
            public bool control()
            {
                if (this.val == "+" || this.val == "-" || this.val == "*" || this.val == "/")
                    return true;
                else
                    return false;
            }
            public Queue<AlberoBinarioIntero> stampaRicorsivaPosticipata( Queue<AlberoBinarioIntero> n)
            {
                if (this.sx == null && this.dx == null)
                {
                    n.Enqueue(this);
                }
                else
                {

                    try
                    {
                        this.sx.stampaRicorsivaPosticipata(n);
                    }
                    catch (Exception e) { }
                    try
                    {
                        this.dx.stampaRicorsivaPosticipata(n);
                    }
                    catch (Exception e) { }
                   n.Enqueue(this);
                }
                return n;
            }


        }
    }
}
