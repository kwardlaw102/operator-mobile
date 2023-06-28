# all constraints should define __call__(self, infoDict), returning a boolean indicating whether the constraint was satisfied

class IntSolutionConstraint:
    def __init__(self):
        pass

    def __call__(self, infoDict):
        solution = infoDict["solution"]
        return int(solution) == solution

class NonNegativeSolutionConstraint:
    def __init__(self):
        pass

    def __call__(self, infoDict):
        return infoDict["solution"] >= 0
    
class LambdaConstraint:
    def __init__(self, func):
        self.func = func

    def __call__(self, infoDict):
        return self.func(infoDict)