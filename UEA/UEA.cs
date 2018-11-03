using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UEA
{
    class UEA
    {
        int dolzina;

        public UEA()
        {

        }

        public UEA(int dolzina)
        {
            this.dolzina = dolzina;
        }

        public int Dolzina { get => dolzina; set => dolzina = value; }

        public double FitnesFunction(int pasti, int premikov, int max_ravna_pot, int pasti_dejanskih, int premikov_dejanskih, int max_ravna_pot_dejanskih)
        {
            return Math.Abs(max_ravna_pot_dejanskih-max_ravna_pot) + Math.Abs(premikov_dejanskih-premikov)+Math.Abs(pasti_dejanskih-pasti);
        }


        public List<List<char>> Ustvari(int pop_size, int level, int pasti, int premikov, int max_ravna_pot, double verjetnost_ovir)
        {
            List<List<char>> ovire = new List<List<char>>();

            //0.generacija
            Random n = new Random();
            for(int i=0; i<pop_size; i++)
            {
                List<char> ovira = new List<char>();
                for(int j=0; j<dolzina; j++)
                {
                    if (n.NextDouble() < verjetnost_ovir)
                        ovira.Add('x');
                    else
                        ovira.Add(' ');
                }
                ovire.Add(ovira);
            }

            //ali je izvedljivo
            if(ovire.Count > 0)
            {
                List<char> trenutni = ovire[0];
                int index = n.Next(0, trenutni.Count);
                trenutni[index] = 'o';

                for (int i = 0; i < ovire.Count-1; i++)
                {
                    trenutni = ovire[i];
                    List<char> naslednji = ovire[i+1];

                    bool is_posible = false;
                    for(int j=0; j<trenutni.Count; j++)
                    {
                        if(trenutni[j] == ' ' && naslednji[j] == ' ')
                        {
                            is_posible = true;
                            trenutni[j] = 'o';
                            naslednji[j] = 'o';
                            break;
                        }
                    }

                    if(is_posible == false)
                    {
                        int vhod = trenutni.IndexOf('o');
                        naslednji[vhod] = 'o';
                    }

                    if(i != 0)
                    {
                        int index1 = -1;
                        int index2 = -1;

                        for(int j=0; j < trenutni.Count; j++)
                        {
                            if (trenutni[j] == 'o')
                            {
                                if (index1 == -1)
                                    index1 = j;
                                else
                                    index2 = j;
                            }
                        }

                        for (int j = index1; j <= index2; j++)
                        {
                            trenutni[j] = 'o';
                        }
                    }
                    
                    
                }
            }
            return ovire;
        }
    }
}
