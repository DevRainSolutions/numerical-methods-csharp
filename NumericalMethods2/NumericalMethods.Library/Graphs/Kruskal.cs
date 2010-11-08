using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//http://c4swimmers.net/portal/kruskalalgo

/*****************************************************************
 *  File    : kruskal.cpp
 *  Purpose : Kruskal's algorithm implementation to find the edges and cost of the
 *            minimal spanning tree.
 *  Author  : Rakesh R and Nanda Kishor K N
 *  Mail Id : knnkishor@yahoo.com, nandakishorkn@rediffmail.com
 *  Website : www.c4swimmers.net ( WEB MASTER )
 *  Group   : c4swimmers@yahoogroups.com   ( GROUP OWNER )
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License as
 *  published by the Free Software Foundation; either version 2 of
 *  the License,or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
 *
 *****************************************************************/
/*
 
#include<iostream.h>

#define MAX 100

class kruskal
{
	private : struct edge_info
		  {
			int u, v, weight;
		  } edge[MAX];
		  int tree[MAX][2], set[MAX];
		  int n;
	public  : int readedges();
		  void makeset();
		  int find(int);
		  void join(int, int);
		  void arrange_edges(int);
		  int spanningtree(int);
		  void display(int);
};

int kruskal :: readedges()
{
	int i, j, k, cost;

	k = 1;
	cout << "\nEnter the number of Vertices in the Graph : ";
	cin  >> n;
	cout << endl;
	for (i = 1; i <= n; i++)
		for (j = 1; j < i; j++)
		{
			cout << "weight[" << i << "][" << j << "] : ";
			cin  >> cost;
			if (cost != 999)
			{
				edge[k].u = i;
				edge[k].v = j;
				edge[k++].weight = cost;
			}
		}
	return (k - 1);
}

void kruskal :: makeset()
{
	int i;
	for (i = 1; i <= n; i++)
		set[i] = i;
}

int kruskal :: find(int vertex)
{
	return (set[vertex]);
}

void kruskal :: join(int v1, int v2)
{
	int i, j;
	if (v1 < v2)
		set[v2] = v1;
	else
		set[v1] = v2;
}

void kruskal :: arrange_edges(int k)
{
	int i, j;
	struct edge_info temp;
	for (i = 1; i < k; i++)
		for (j = 1; j <= k - i; j++)
			if (edge[j].weight > edge[j + 1].weight)
			{
				temp = edge[j];
				edge[j] = edge[j + 1];
				edge[j + 1] = temp;
			}
}

int kruskal :: spanningtree(int k)
{
	int i, t, sum;
	arrange_edges(k);
	t = 1;
	sum = 0;
	for (i=1;i<=k;i++)
	cout<<edge[i].u<<edge[i].v<<" "<<edge[i].weight<<endl;getch();
	for (i = 1; i <= k; i++)
		if (find (edge[i].u) != find (edge[i].v))
		{
			tree[t][1] = edge[i].u;
			tree[t][2] = edge[i].v;
			sum += edge[i].weight;
			join (edge[t].u, edge[t].v);
			t++;
		}
	return sum;
}

void kruskal :: display(int cost)
{
	int  i;
	cout << "\nThe Edges of the Minimum Spanning Tree are\n\n";
	for (i = 1; i < n; i++)
		cout << tree[i][1] << " - " << tree[i][2] << endl;
	cout << "\nThe Cost of the Minimum Spanning Tree is : " << cost;
}

int main()
{
	int ecount, totalcost;
	kruskal k;
	ecount = k.readedges();
	k.makeset();
	totalcost = k.spanningtree(ecount);
	k.display(totalcost);
	return 0;
}


*/

namespace System.NumericalMethods
{
    public class EdgeInfo
    {
        public int u { get; set; }
        public int v { get; set; }
        public int weight { get; set; }
    }

    public class Kruskal
    {

        public Kruskal(EdgeInfo[] edges, int n) 
        {
            this.Edges = edges;
            this.N = n;
            this.tree = new int[MAX][];
            this.makeset();
        }

        private const int MAX = 100;

        EdgeInfo[] Edges = new EdgeInfo[MAX];
        int[][] tree;
        int[] set = new int[MAX];
        int N;

        /*
    int readedges()
    {
        int i, j, k, cost;

        k = 1;
        //cout << "\nEnter the number of Vertices in the Graph : ";
        //cin  >> n;
        cout << endl;
        for (i = 1; i <= n; i++)
            for (j = 1; j < i; j++)
            {
                cout << "weight[" << i << "][" << j << "] : ";
                cin  >> cost;
                if (cost != 999)
                {
                    edge[k].u = i;
                    edge[k].v = j;
                    edge[k++].weight = cost;
                }
            }
        return (k - 1);
    }

         */
        void makeset()
        {
            int i;
            for (i = 1; i <= N; i++)
                set[i] = i;
        }

        int find(int vertex)
        {
            return (set[vertex]);
        }

        void join(int v1, int v2)
        {
            int i, j;
            if (v1 < v2)
                set[v2] = v1;
            else
                set[v1] = v2;
        }

        void arrange_edges(int k)
        {
            int i, j;
            EdgeInfo temp;
            for (i = 1; i < k; i++)
                for (j = 1; j <= k - i; j++)
                    if (Edges[j].weight > Edges[j + 1].weight)
                    {
                        temp = Edges[j];
                        Edges[j] = Edges[j + 1];
                        Edges[j + 1] = temp;
                    }
        }

        public int spanningtree(int k)
        {
            int i, t, sum;
            arrange_edges(k);
            t = 1;
            sum = 0;
            for (i = 1; i <= k; i++)
                //cout<<edge[i].u<<edge[i].v<<" "<<edge[i].weight<<endl;getch();
                for (i = 1; i <= k; i++)
                    if (find(Edges[i].u) != find(Edges[i].v))
                    {
                        tree[t][1] = Edges[i].u;
                        tree[t][2] = Edges[i].v;
                        sum += Edges[i].weight;
                        join(Edges[t].u, Edges[t].v);
                        t++;
                    }
            return sum;
        }


        public void display(int cost)
        {
            int i;
            Console.WriteLine("The Edges of the Minimum Spanning Tree are");
            for (i = 1; i < N; i++)
            {
                Console.WriteLine("{0} - {1}", tree[i][1], tree[i][2]);
            }
            Console.WriteLine("The Cost of the Minimum Spanning Tree is : {0}", cost);
        }

        /*
        int main()
        {
            int ecount, totalcost;
            kruskal k;
            ecount = k.readedges();
            k.makeset();
            totalcost = k.spanningtree(ecount);
            k.display(totalcost);
            return 0;
        }
         */

    }
}


