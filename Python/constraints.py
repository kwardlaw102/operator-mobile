# all constraints should define __call__(self, problem), returning a boolean indicating whether the constraint was satisfied

class IntSolutionConstraint:
    def __call__(self, problem):
        solution = problem.solution()
        return int(solution) == solution

class NonNegativeSolutionConstraint:
    def __call__(self, problem):
        return problem.solution() >= 0
    
class LambdaConstraint:
    def __init__(self, func):
        self.func = func

    def __call__(self, problem):
        return self.func(problem)

# Removes a class of trivial problems where the solution is zero and a zero operand is present
class NoZeroMultiplicationConstraint:
    def __call__(self, problem):
        zeroSolution = problem.solution() == 0
        hasZeroNumber = False
        for number in problem.numbers:
            if number == 0:
                hasZeroNumber = True
                break
        return not (zeroSolution and hasZeroNumber)
    
class HasOperatorsConstraint:
    def __init__(self, *required_operators):
        self.required_operators = required_operators

    def __call__(self, problem):
        for required_operator in self.required_operators:
            if not required_operator in problem.operators:
                return False
        return True
    
class HasNumbersConstraint:
    def __init__(self, *required_numbers):
        self.required_numbers = required_numbers

    def __call__(self, problem):
        for required_number in self.required_numbers:
            if not required_number in problem.numbers:
                return False
        return True

# TODO: throw exception if not enough operators or numbers for constraints

class Or:
    def __init__(self, *constraints):
        self.constraints = constraints
    
    def __call__(self, problem):
        return any([constraint(problem) for constraint in self.constraints])
    
class And:
    def __init__(self, *constraints):
        self.constraints = constraints

    def __call__(self, problem):
        return all([constraint(problem) for constraint in self.constraints])
    
class Not:
    def __init__(self, constraint):
        self.constraint = constraint
    
    def __call__(self, problem):
        return not self.constraint(problem)


if __name__ == "__main__":
    pass