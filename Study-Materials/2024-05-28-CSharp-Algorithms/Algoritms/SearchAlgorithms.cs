public class SearchAlgorithms 
{

/*
Search algorithms
*/

/* Linear search

two parameter variables
    - int[] arr -> sample array
    - int x -> the index of the elements we are searching through


*/

public int LinearSearch(int[] arr, int x) 
{
    // simply search through the array until we hit the index of x.
    for(int i = 0; i < arr.Length; i++) 
    {
        if(arr[i] == x) 
        {
            return i;
        }
    }
    return -1; 
}

/*
Binary search algorithm

paramater variables we need:
    - int[] arr -> sample array
    - int x -> value to find

local variables:
    - left-side variable -> int left
    - right-side variable -> int right => arr.Lenght - 1;
    - middle variable -> int middle => (1 + (r - 1) / 2)    

*/

public int BinarySearch(int[] arr, int x) 
{
    int left = 0;
    int right = arr.Length - 1;

    while(left <= right) 
    {
        int middle = left + (right - 1) / 2;
        if(arr[middle] == x) 
        {
            return middle;
        }
        if(arr[middle] < x) 
        {
            left = middle + 1;
        }
        else 
        {
            right = middle - 1;
        }

    }
    return -1;
}

}
