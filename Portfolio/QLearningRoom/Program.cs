using System;

namespace Testing
{

    class QLearning
    {

        private static double GAMMA = 0.8;
        private static int ITERATIONS = 10;
        private static int[] INITIAL_STATES = { 0, 1, 2, 3, 4, 5, 6, 7 };
        private static int Q_SIZE = INITIAL_STATES.Length;

        private static int[,] R = {{-1, 0, -1, -1, -1, -1, -1, -1},
                                    {0, -1, 0, -1, -1, -1, -1, -1},
                                    {-1, 0, -1, 0, -1, -1, 0, -1},
                                    {-1, -1, 0, -1, 0, -1, -1, -1},
                                    {-1, -1, -1, 0, -1, 100, -1, -1},
                                    {-1, -1, -1, -1, 0, 100, -1, -1},
                                    {-1, -1, 0, -1, -1, -1, -1, 0},
                                    {-1, -1, -1, -1, -1, -1, 0, -1}};

        private static int[,] q = new int[Q_SIZE, Q_SIZE];
        private static int currentState = 0;

        private static int GOAL = 5;

        public static void Main(String[] args)
        {
            train();
            test();
        }

        private static void train()
        {
            initialize();

            // Perform training, starting at all initial states.
            for (int j = 0; j < ITERATIONS; j++)
            {
                for (int i = 0; i < Q_SIZE; i++)
                {
                    episode(INITIAL_STATES[i]);
                }
            }
            Console.WriteLine("Q Matrix values:");

            for (int i = 0; i < Q_SIZE; i++)
            {
                for (int j = 0; j < Q_SIZE; j++)
                {
                    Console.Write(q[i, j] + ",\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void test()
        {
            // Perform tests, starting at all initial states.
            Console.WriteLine("Shortest routes from initial states:");
            for (int i = 0; i < Q_SIZE; i++)
            {
                currentState = INITIAL_STATES[i];
                int newState = 0;
                do
                {
                    newState = maximum(currentState, true);
                    Console.Write(currentState + ", ");
                    currentState = newState;
                } while (currentState < GOAL);
                Console.WriteLine(GOAL);
            }
        }

        private static void episode(int initialState)
        {
            currentState = initialState;

            // Travel from state to state until goal state is reached.
            do
            {
                chooseAnAction();
            } while (currentState == GOAL);

            // When currentState = GOAL, Run through the set once more for convergence.
            for (int i = 0; i < Q_SIZE; i++)
            {
                chooseAnAction();
            }
        }

        private static void chooseAnAction()
        {
            int possibleAction = 0;

            // Randomly choose a possible action connected to the current state.
            possibleAction = getRandomAction(Q_SIZE);

            if (R[currentState, possibleAction] >= 0)
            {
                q[currentState, possibleAction] = reward(possibleAction);
                currentState = possibleAction;
            }
        }

        private static int getRandomAction(int upperBound)
        {
            int action = 0;
            bool choiceIsValid = false;

            // Randomly choose a possible action connected to the current state.
            while (choiceIsValid == false)
            {
                // Get a random value between 0(inclusive) and 6(exclusive).
                action = new Random().Next(upperBound);
                if (R[currentState, action] > -1)
                {
                    choiceIsValid = true;
                }
            }

            return action;
        }

        private static void initialize()
        {
            for (int i = 0; i < Q_SIZE; i++)
            {
                for (int j = 0; j < Q_SIZE; j++)
                {
                    q[i, j] = 0;
                }
            }
        }

        private static int maximum(int State, bool ReturnIndexOnly)
        {
            // If ReturnIndexOnly = True, the Q matrix index is returned.
            // If ReturnIndexOnly = False, the Q matrix value is returned.
            int winner = 0;
            bool foundNewWinner = false;
            bool done = false;

            while (!done)
            {
                foundNewWinner = false;
                for (int i = 0; i < Q_SIZE; i++)
                {
                    if (i != winner)
                    {             // Avoid self-comparison.
                        if (q[State, i] > q[State, winner])
                        {
                            winner = i;
                            foundNewWinner = true;
                        }
                    }
                }

                if (foundNewWinner == false)
                {
                    done = true;
                }
            }

            if (ReturnIndexOnly == true)
            {
                return winner;
            }
            else
            {
                return q[State, winner];
            }
        }

        private static int reward(int Action)
        {
            return (int)(R[currentState, Action] + (GAMMA * maximum(Action, false)));
        }
    }
}