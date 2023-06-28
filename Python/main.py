from constraints import *
from problem import Problem, Operator

def batch_generate_problems(num_problems, num_operators, constraints=[]):
    problems = []
    solutions = []
    for i in range(num_problems):
        problem = Problem.generate(num_operators, constraints)
        problems.append(problem.full_str(whitespace = " _ ", with_solution = True))
        solutions.append(problem.full_str(with_solution = True))
    return problems, solutions

def game_loop(constraints=[]):
    play_again = True
    while play_again:
        num_operators = int(input("Enter a number of operators greater than zero: "))
        print("Generating problem with " + str(num_operators) + " operators...")
        problem = Problem.generate(num_operators, constraints)
        print(problem.full_str(whitespace = " _ "))
        print(problem.solution())

def test_game_loop():
    constraints = [LambdaConstraint(lambda problem: problem.solution() == 0),
                   NoZeroMultiplicationConstraint(), 
                   NonNegativeSolutionConstraint(), 
                   IntSolutionConstraint(),
                   ]
    game_loop(constraints)

def generate_prototype_problems():
    problems = []
    solutions = []

    p, s = batch_generate_problems(30, 1)
    problems.extend(p)
    solutions.extend(s)

    p, s = batch_generate_problems(30, 2)
    problems.extend(p)
    solutions.extend(s)

    p, s = batch_generate_problems(30, 3)
    problems.extend(p)
    solutions.extend(s)

    for i in range(len(problems)):
        problems[i] = problems[i] + "\n"
    for i in range(len(solutions)):
        solutions[i] = solutions[i] + "\n"

    with open("problems.txt", "w") as f:
        f.writelines(problems)
        f.close()

    with open("solutions.txt", "w") as f:
        f.writelines(solutions)
        f.close()

def test_constraints():
    constraints = [HasOperatorsConstraint(Operator.DIVIDE, Operator.SUBTRACT),
                   Not(Or(HasNumbersConstraint(2, 7), HasNumbersConstraint(1, 9))),
                   IntSolutionConstraint(),
                   ]
    _, s = batch_generate_problems(10, 2, constraints)
    for problem in s:
        print(problem)

if __name__ == "__main__":
    #test_constraints()
    test_game_loop()
    #generate_prototype_problems()
    

# TODO: each operator has a priority (1, 2, 3, 4); USER sets the priority to place parentheses
# if priority is not set, order of operations will be followed

# TODO: add timeout to generation function