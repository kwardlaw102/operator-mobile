# all constraints should define __call__(self, infoDict), returning a boolean indicating whether the constraint was satisfied

class IntSolutionConstraint:
    def __call__(self, infoDict):
        solution = infoDict["solution"]
        return int(solution) == solution

class NonNegativeSolutionConstraint:
    def __call__(self, infoDict):
        return infoDict["solution"] >= 0
    
class LambdaConstraint:
    def __init__(self, func):
        self.func = func

    def __call__(self, infoDict):
        return self.func(infoDict)

# Removes a class of trivial problems where the solution is zero and a zero operand is present
class NoZeroMultiplicationConstraint:
    def __call__(self, infoDict):
        zeroSolution = infoDict["solution"] == 0
        hasZeroNumber = False
        for number in infoDict["numbers"]:
            if number == 0:
                hasZeroNumber = True
                break
        return not (zeroSolution and hasZeroNumber)
    
class HasOperatorsConstraint:
    def __init__(self, *required_operators):
        self.required_operators = required_operators

    def __call__(self, infoDict):
        for required_operator in self.required_operators:
            if not required_operator in infoDict["operators"]:
                return False
        return True
    
class HasNumbersConstraint:
    def __init__(self, *required_numbers):
        self.required_numbers = required_numbers

    def __call__(self, infoDict):
        for required_number in self.required_numbers:
            if not required_number in infoDict["numbers"]:
                return False
        return True

# TODO: throw exception if not enough operators or numbers for constraints

class Or:
    def __init__(self, *constraints):
        self.constraints = constraints
    
    def __call__(self, infoDict):
        return any([constraint(infoDict) for constraint in self.constraints])
    
class And:
    def __init__(self, *constraints):
        self.constraints = constraints

    def __call__(self, infoDict):
        return all([constraint(infoDict) for constraint in self.constraints])
    
class Not:
    def __init__(self, constraint):
        self.constraint = constraint
    
    def __call__(self, infoDict):
        return not self.constraint(infoDict)
