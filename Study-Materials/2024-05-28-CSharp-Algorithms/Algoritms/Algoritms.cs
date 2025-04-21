
public class Algorithms 
{
    // bubble sort implementation
    // Bubble sort is not generally considered a fast algorithm
    public void BubbleSort(int[] arr) 
    {
        // assign a variable n to the lenght of the array.
        int n = arr.Length;
        // use nested for loops to iterate through the array and split the array up, then sort them
        for(int i = 0; i < n - 1; i++) 
        {
            for(int j = 0; j < n - 1; j++) 
            {
                if(arr[j] > arr[j + 1]) 
                {
                    /* if the index of j in the array
                       is greater than j+1
                       we swap the values of arr[j] & arr[j+1]
                    */
                    int temporary = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temporary;
                }
            }
        }
    }
        /*
        Quicksort
        we need three paramaters:
            - int[] arr
            - int low
            - int high

        we also need some local variables:
            - we need a int pivot -> int pivot = arr[high]  -> this variable is the one that utilizes
            the recursion.
            - we need an iterator -> int i
        
        we also need to do a little recursion


        */
    
        public void Quicksort(int[] arr, int low, int high) 
        {
            if(low < high) 
            {
                // do the recursive calls to the methods
                int piv1 = Recursion(arr, low, high);
                // recursively call Quicksort() with sample array, low and piv1 - 1
                Quicksort(arr, low, piv1 - 1);
                Quicksort(arr, piv1 + 1, high);
            }
        }

    private int Recursion(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);
        for(int j = low; j < high; j++) 
        {
            if(arr[j] < pivot) 
            {
                // first swap
                i++;
                int temporary = arr[i];
                arr[i] = arr[j];
                arr[j] = temporary;
            }
        }
        // swap a second time
        int swapTemporary = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = swapTemporary;
        return i + 1; 
    }
}
