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