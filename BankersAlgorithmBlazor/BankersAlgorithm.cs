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

        public static BankersAlgorithmResult IsSafe(int[] available,
            int[,] allocationMatrix, int[,] needMatrix)
        {
            int numberOfProcesses = allocationMatrix.GetLength(0);
            int numberOfResources = allocationMatrix.GetLength(1);

            bool[] processFinished = new bool[numberOfProcesses];
            int[] safeSequence = new int[numberOfProcesses];
            Array.Fill(safeSequence, -1);
            int[] work = new int[numberOfResources];
            Array.Copy(available, work, available.Length);

            int[,] allAvailableRecords = new int[numberOfProcesses + 1, numberOfResources];
            for (int i = 0; i < 3; i++)
            {
                allAvailableRecords[0, i] = work[i];
            }

            int count = 0;
            while (count < numberOfProcesses)
            {
                bool atLeastOneProcessCanRun = false;
                for (int i = 0; i < numberOfProcesses; i++)
                {
                    if (processFinished[i])
                    {
                        continue;
                    }

                    bool canRunProcess = true;
                    for (int j = 0; j < numberOfResources; j++)
                    {
                        if (needMatrix[i, j] > work[j])
                        {
                            canRunProcess = false;
                            break;
                        }
                    }

                    if (canRunProcess)
                    {
                        safeSequence[count++] = i;
                        processFinished[i] = true;
                        atLeastOneProcessCanRun = true;
                        for (int j = 0; j < numberOfResources; j++)
                        {
                            work[j] += allocationMatrix[i, j];
                        }
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        allAvailableRecords[count, j] = work[j];
                    }
                }

                if (atLeastOneProcessCanRun == false)
                {
                    break;
                }
            }

            bool isSafe = count == numberOfProcesses;
            return new BankersAlgorithmResult(isSafe, safeSequence, allAvailableRecords);
        }
    }
}
