int[] numbers = { 1, 2, 3 };

Console.WriteLine(numbers is [1, 2, 3]);  // True
Console.WriteLine(numbers is [1, 2, 4]);  // False
Console.WriteLine(numbers is [1, 2, 3, 4]);  // False
Console.WriteLine(numbers is [0 or 1, <= 2, >= 3]);  // True


int[][] points = { new int[] { 0, 0 }, new int[] { 50, 0 }, new int[] { 0, 50 }, new int[] { 50, 50 } };
int[] point = { 10, 10 }; 

