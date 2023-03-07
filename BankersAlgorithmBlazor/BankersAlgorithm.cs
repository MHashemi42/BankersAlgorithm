namespace BankersAlgorithmBlazor
{
    public static class BankersAlgorithm
    {
        public static void CalculateNeedMatrix(int numberOfProcesses, int numberOfResources,
            int[,] maxMatrix, int[,] allocationMatrix, int[,] needMatrix)
        {
            for (int i = 0; i < numberOfProcesses; i++)
            {
                for (int j = 0; j < numberOfResources; j++)
                {
                    needMatrix[i, j] = maxMatrix[i, j] - allocationMatrix[i, j];
                }
            }
        }

        public static void CalculateAvailable(int numberOfProcesses, int numberOfResources,
            int[,] allocationMatrix, int[] available)
        {
            for (int i = 0; i < numberOfProcesses; i++)
            {
                for (int j = 0; j < numberOfResources; j++)
                {
                    available[j] = available[j] - allocationMatrix[i, j];
                }
            }
        }

        public static bool IsSafe(int numberOfProcesses, int numberOfResources,
            int[] available, int[,] allocationMatrix, int[,] needMatrix,
            int[] safeSequence)
        {
            bool[] visited = new bool[numberOfProcesses];

            int count = 0;
            while (count < numberOfProcesses)
            {
                bool flag = false;
                for (int i = 0; i < numberOfProcesses; i++)
                {
                    if (visited[i])
                    {
                        continue;
                    }

                    int j;
                    for (j = 0; j < numberOfResources; j++)
                    {
                        if (needMatrix[i, j] > available[j])
                        {
                            break;
                        }
                    }

                    if (j == numberOfResources)
                    {
                        safeSequence[count++] = i;
                        visited[i] = true;
                        flag = true;
                        for (j = 0; j < numberOfResources; j++)
                        {
                            available[j] += allocationMatrix[i, j];
                        }
                    }
                }

                if (flag == false)
                {
                    break;
                }
            }

            return count >= numberOfProcesses;
        }
    }
}
