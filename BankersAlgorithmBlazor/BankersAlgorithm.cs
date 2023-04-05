namespace BankersAlgorithmBlazor
{
    public static class BankersAlgorithm
    {
        public static void CalculateNeedMatrix(int[,] maxMatrix, int[,] allocationMatrix,
            int[,] needMatrix)
        {
            int numberOfProcesses = allocationMatrix.GetLength(0);
            int numberOfResources = allocationMatrix.GetLength(1);

            for (int i = 0; i < numberOfProcesses; i++)
            {
                for (int j = 0; j < numberOfResources; j++)
                {
                    needMatrix[i, j] = maxMatrix[i, j] - allocationMatrix[i, j];
                }
            }
        }

        public static void CalculateAvailable(int[,] allocationMatrix, int[] available)
        {
            int numberOfProcesses = allocationMatrix.GetLength(0);
            int numberOfResources = allocationMatrix.GetLength(1);

            for (int i = 0; i < numberOfProcesses; i++)
            {
                for (int j = 0; j < numberOfResources; j++)
                {
                    available[j] = available[j] - allocationMatrix[i, j];
                }
            }
        }

        public static (bool isSafe, int[] safeSequence) IsSafe(int[] available,
            int[,] allocationMatrix, int[,] needMatrix)
        {
            int numberOfProcesses = allocationMatrix.GetLength(0);
            int numberOfResources = allocationMatrix.GetLength(1);
            bool[] finished = new bool[numberOfProcesses];
            int[] safeSequence = new int[numberOfProcesses];
            Array.Fill(safeSequence, -1);

            int count = 0;
            while (count < numberOfProcesses)
            {
                bool flag = false;
                for (int i = 0; i < numberOfProcesses; i++)
                {
                    if (finished[i])
                    {
                        continue;
                    }

                    bool canRunProcess = true;
                    for (int j = 0; j < numberOfResources; j++)
                    {
                        if (needMatrix[i, j] > available[j])
                        {
                            canRunProcess = false;
                            break;
                        }
                    }

                    if (canRunProcess)
                    {
                        safeSequence[count++] = i;
                        finished[i] = true;
                        flag = true;
                        for (int j = 0; j < numberOfResources; j++)
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

            bool isSafe = count >= numberOfProcesses;
            return (isSafe, safeSequence);
        }
    }
}
