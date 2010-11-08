using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.NumericalMethods
{
    class Kruskal2
    {
    }
}


 /********************************************************************
	created:	2008/12/23
	created:	23:12:2008   9:03
	filename: 	General\Minimum Spanning Tree.cpp
	file path:	General
	file base:	Minimum Spanning Tree
	file ext:	cpp
	author:		Gбbor Bernбt
	
	purpose:	This is a C/C++ implementation of the Prim and Kruskall 
				Algorithm Presenting the creation of the Minimal Spanning Tree
*********************************************************************/

/********************************************************************
	created:	23:12:2008   9:07	
	author:		Gбbor Bernбt
	edited: 	-
*********************************************************************/
/*

#include <stdio.h>
#include <stdlib.h>
#include <memory.h>

// Create the edge type
struct Edge
{
	int from;
	int to; 
	int weight; 
	bool inTheCut; 
};
typedef Edge* pEdge; 


// adjacency list
struct ListIt
{
	int value;
	ListIt *p_next;
}; 

// typedef for the pointer type
typedef ListIt* pListIt;

pEdge edges;
pEdge edgesPr; 

int n,m; 

int *tree; 
int *ancient;
int **distanceMatrix; 


pListIt* adjacencyList; // Here we save the relations.... A 100000 is the maximum vertexes


void read(); 
void print(); 
void Kruskal(); 
int Prim(int at);
bool add(pListIt &dest, int val); 
int compareEdges(const void* el1, const void* el2);
int min_vag();

int main()
{
	read(); 
	printf("\n"); 
	print(); 
	
	//Kruskal();

	printf("\n\n With Prims Technique: \n\n"); 
	Prim(1);

	return 0;
}

//************************************
// Method:    read - public  access
// Returns:   void
//
// Purpose:   Read in the data from In.txt
//************************************
void read()
{
	FILE *f; 
	freopen_s(&f, "In.txt", "r", stdin); 
	freopen_s(&f, "Out.txt", "w", stdout); 

	scanf_s("%d", &n); 
	scanf_s("%d", &m); 	

	tree = (int*) calloc(n+1, sizeof(int)); 
	ancient = (int*) calloc(n+1, sizeof(int)); 
	edges = (pEdge)malloc(m*sizeof(Edge)); 
	adjacencyList = (pListIt*) calloc(n+1,sizeof(pListIt));

	distanceMatrix = (int**) calloc(n+1, sizeof(int*)); 
	for (int j = 1; j <=n; ++j)
	{
		distanceMatrix[j] = (int*) calloc(n+1, sizeof(int)); 
	}


	int from,to,distance; 

	for(int i =0; i< m; ++i)
	{
		scanf_s("%d%d%d", &from, &to, & distance);

		add(adjacencyList[from], to);
		add(adjacencyList[to], from);

		edges[i].from = from;
		edges[i].to = to;
		edges[i].weight = distance;
	}
}

//************************************
// Method:    print - public  access
// Parameter: 
// Returns:   void
//
// Purpose:   Print the list of edges
//************************************
void print()
{
	printf("\n The list of the edges:\n "); 
	for(int i =0; i < m; ++i)
	{	
		if(!(i % 1)) // how many in a row... for now 1 edge per row
		{
			printf("\n");
		}
		printf(" %d ~ %d -> %d   ", edges[i].from, edges[i].to, edges[i].weight);
	}

	printf("\n");

}

void Kruskal()
{
	int i = 0, overallWeight = 0 , j =0; 
	int fromTree = 0, toTree = 0, changeNr = 0; 


	// sort the edge list
	qsort((void*)edges,m,sizeof(Edge),compareEdges);


	printf("\n"); 
	print(); 
	printf("\n"); 

	printf("\nThe list of the edges with the algorithm of Kruskall:\n"); 


	// build up the individual list
	for ( i =1;  i <= n; ++i)
		tree[i] = i;


	for ( i= 0; i < m && changeNr < n; ++i)
	{
		fromTree = tree[edges[i].from];
		toTree = tree[edges[i].to];

		if( fromTree != toTree)
		{
			++changeNr; 
			overallWeight += edges[i].weight; 
			printf("   %d) %d ~ %d -> %d  \n", changeNr, edges[i].from, edges[i].to, edges[i].weight);
			
			//Union of the trees
			for ( j = 0; j < m; ++j)
				if(tree[j] == toTree)
					tree[j] = fromTree;
		}	
	}
	printf( " \n The minimal weight %d ", overallWeight);  

}

//************************************
// Method:    compareEdges - public  access
// Parameter: 
//			  const void * edge1
//			  const void * edge2
// Returns:   int
//
// Purpose:   Compare two edge types according to their weight
//************************************
int compareEdges(const void* edge1, const void * edge2)
{
	pEdge first = (pEdge)edge1;
	pEdge second = (pEdge)edge2;

	return first->weight - second->weight; 
}

//************************************
// Method:    Prim - public  access
// Parameter: 
//			  int at
// Returns:   int
//
// Purpose:   Prims Technique to generate the minimal spanning tree
//************************************
int Prim(int at)
{
	int i = 0; 
	
	memset(tree, 0, (n+1)*sizeof(int)); 

	tree[at] = 1; 
	ancient[at]  = 0 ;
	

	// sort the edge list
	qsort((void*)edges,m,sizeof(Edge),compareEdges);


	// build up the tree
	for (i = 0; i < m; ++i)
	{
		distanceMatrix[edges[i].from][edges[i].to  ] = i;
		distanceMatrix[edges[i].to  ][edges[i].from] = i;

		if (edges[i].from == at || edges[i].to == at)
		{
			edges[i].inTheCut = true;
		}
		else
		{
			edges[i].inTheCut = false;
		}
	}

	int overallWeight = 0; 
	int curent = 0; 
	int addedVertex = 1; 
	int neighborVertex = 0;
	int neighborEdge = 0; 
	pListIt p = NULL; 
	int j = 0; 

	for(i = 1; i < n; ++i)
	{
		for ( j =0 ; j < m; ++j)
		{
			if (edges[j].inTheCut)
			{
				curent = j; 
				break;
			}
		}


		printf( "   %d)  %d ~ %d -> %d \n",i, edges[curent].from,edges[curent].to, edges[curent].weight);

		overallWeight += edges[curent].weight;

		if (tree[edges[curent].from] == 1)
		{
			addedVertex = edges[curent].to;
			ancient[addedVertex] = edges[curent].from; 
		}

		tree[addedVertex] = 1; 

		for (p = adjacencyList[addedVertex]; p != NULL; p = p -> p_next)
		{
			neighborVertex = p->value;
			neighborEdge = distanceMatrix[addedVertex][neighborVertex];

			if (tree[neighborVertex] == 1)
				edges[neighborEdge].inTheCut = false; // reset if both ends are there
			else
				edges[neighborEdge].inTheCut = true; // set if only one is there

		}

	}

	printf( "\n\n The overall Weight: %d " , overallWeight);
	return 0;
}


// Here we need to make sure that the items are added in 	 // correct order
bool add(pListIt &dest, int val)
{
	//create the item
	pListIt p;
	p = (pListIt) malloc(sizeof(ListIt));
	p -> value = val;	

	if(!dest)  // first item addition 
	{
		p -> p_next = NULL;
		dest = p;	
	}
	else
	{	
		pListIt find = dest; 
		pListIt at = NULL; 

		// first find the first greater number, insert before
		while(find && find->value <= val)
		{
			if( find->value == val) // do not add a duplicate
			{
				free(p);
				return false;
			}

			at = find; 
			find = find->p_next;					
		}

		// insert at 
		if (at) // insert at a valid point
		{
			p->p_next = at->p_next;
			at->p_next = p; 				
		}
		else // insert at the start
		{
			p->p_next = dest; 
			dest = p; 
		}
	}

	return true; 
}

/*
Sample Input: => In.txt

9 13
1 2 10
1 9 60
1 3 30
2 9 50
2 3 20
3 9 40
3 4 70
4 5 110
3 5 120
3 6 100
5 6 130
6 8 90
6 7 80



Sample Output: => Out.txt

The list of the edges:

1 ~ 2 -> 10   
1 ~ 9 -> 60   
1 ~ 3 -> 30   
2 ~ 9 -> 50   
2 ~ 3 -> 20   
3 ~ 9 -> 40   
3 ~ 4 -> 70   
4 ~ 5 -> 110   
3 ~ 5 -> 120   
3 ~ 6 -> 100   
5 ~ 6 -> 130   
6 ~ 8 -> 90   
6 ~ 7 -> 80   


The list of the edges:

1 ~ 2 -> 10   
2 ~ 3 -> 20   
1 ~ 3 -> 30   
3 ~ 9 -> 40   
2 ~ 9 -> 50   
1 ~ 9 -> 60   
3 ~ 4 -> 70   
6 ~ 7 -> 80   
6 ~ 8 -> 90   
3 ~ 6 -> 100   
4 ~ 5 -> 110   
3 ~ 5 -> 120   
5 ~ 6 -> 130   


The list of the edges with the algorithm of Kruskall:
1) 1 ~ 2 -> 10  
2) 2 ~ 3 -> 20  
3) 3 ~ 9 -> 40  
4) 3 ~ 4 -> 70  
5) 6 ~ 7 -> 80  
6) 6 ~ 8 -> 90  
7) 3 ~ 6 -> 100  
8) 4 ~ 5 -> 110  

The minimal weight 520 

With Prims Technique: 

1)  1 ~ 2 -> 10 
2)  2 ~ 3 -> 20 
3)  3 ~ 9 -> 40 
4)  3 ~ 4 -> 70 
5)  3 ~ 6 -> 100 
6)  6 ~ 7 -> 80 
7)  6 ~ 8 -> 90 
8)  4 ~ 5 -> 110 


The overall Weight: 520 

*/

