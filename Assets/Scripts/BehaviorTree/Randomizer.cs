using System;

namespace BehaviorTreeSpace
{
    public class Randomizer : Composite
    {
        int index;
        Random rnd;
        public Randomizer()
        {
            
            UpdateAct = () =>
            {
                Status s = GetChild(index).Update();
                if (s == Status.INVALID) return Status.INVALID;
                else if (s == Status.SUCCESS)
                {
                    index = chooseNewIndex();
                    return Status.SUCCESS;
                }
                else if (s == Status.FAILURE)
                {
                    index = chooseNewIndex();
                    return Status.FAILURE;
                }
                else
                {
                    return Status.RUNNING;
                }
            };

            Initialize = () =>
            {
                rnd = new Random(GetHashCode());
                index = chooseNewIndex();
            };
        }

        private int chooseNewIndex()
        {
            return rnd.Next(0, ChildCount);
        }
    }

}
