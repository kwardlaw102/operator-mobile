from random import randint, randrange, choice
from enum import IntEnum

class Operator(IntEnum):
    ADD = 0
    SUBTRACT = 1
    MULTIPLY = 2
    DIVIDE = 3

    @staticmethod
    def random():
        return choice(list(Operator))
    
    def symbol(self):
        return operator_dict[self]    
    
operator_dict = dict([(Operator.ADD, " + "),
                      (Operator.SUBTRACT, " - "),
                      (Operator.MULTIPLY, " * "),
                      (Operator.DIVIDE, " / ")])

class Problem:
    def __init__(self):
        self.numbers = []
        self.operators = []

    def full_str(self, whitespace = None, with_solution = False):
        if (len(self.numbers) == 0):
            return "0"
        output = str(self.numbers[0])
        for i in range(len(self.operators)):
            output += self.operators[i].symbol() if whitespace is None else whitespace
            output += str(self.numbers[i+1])
        if with_solution:
            output += " = " + str(self.solution())
        return output
    
    def solution(self):
        solution = eval(self.full_str())
        if solution == int(solution):
            solution = int(solution) # remove unnecessary decimal in string representation of whole numbers
        return solution
    
    def satisfies_constraints(self, constraints):
        return all([constraint(self) for constraint in constraints])

    @staticmethod
    def generate(num_operators = 1, constraints = []):
        # problem must have at least 1 operator
        if num_operators < 1:
            print("number of operators must be greater than 1")
            return None # TODO: raise exception

        problem = Problem()

        while True:
            problem.numbers = [randint(0, 9) for _ in range(num_operators + 1)]
            problem.operators = [Operator.random() for _ in range(num_operators)]

            try:
                problem.solution()
            except ZeroDivisionError:
                # print("Division by zero")
                continue

            if problem.satisfies_constraints(constraints): # FIXME: does this use shortcircuiting?
                break
        
        return problem

if __name__ == "__main__":
    problem = Problem()
    problem.numbers = [1, 2, 3]
    problem.operators = [Operator.ADD, Operator.MULTIPLY]
    print(problem.full_str())
    print(problem.full_str(whitespace = " _ ", with_solution = True))
    
    print("---")
    for _ in range(10):
        problem = Problem.generate(num_operators = 3)
        print(problem.full_str(with_solution = True))

    print("---")
    from constraints import NonNegativeSolutionConstraint, IntSolutionConstraint
    constraints = [NonNegativeSolutionConstraint(), IntSolutionConstraint()]
    for _ in range(10):
        problem = Problem.generate(num_operators = 2, constraints = constraints)
        print(problem.full_str(with_solution = True))