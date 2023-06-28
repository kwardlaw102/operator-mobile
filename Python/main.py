from random import randint

operator_dict = dict([(0, " + "),
                      (1, " - "),
                      (2, " * "),
                      (3, " / ")])

def generate_numbers(numNumbers):
    return [randint(0, 9) for _ in range(numNumbers)]

def generate_operators(numOperators):
    return [operator_dict[randint(0, 3)] for _ in range(numOperators)]

def generate_eval_string(numbers, operators, whitespace=None):
    eval_string = ""
    for i in range(len(operators)):
        eval_string += str(numbers[i])
        eval_string += str(operators[i]) if whitespace is None else str(whitespace)
    eval_string += str(numbers[len(numbers)-1])
    return eval_string

def batch_generate_problems(num_problems, num_operators):
    problems = []
    solutions = []
    for i in range(num_problems):
        numbers, operators, eval_string, solution = generate(num_operators)
        full_string = generate_eval_string(numbers, operators, " _ ") + " = " + str(int(solution))
        problems.append(full_string)
        solutions.append(eval_string + " = " + str(int(solution)))
    return problems, solutions

def generate(numOperators=1):
    failed_generate = True

    while failed_generate:
        # problem must have at least 1 operator
        if numOperators < 1:
            print("number of operators must be greater than 1")
            return
        
        # generate random integers
        numbers = generate_numbers(numOperators + 1)

        # generate random operators
        operators = generate_operators(numOperators)

        # create eval string
        eval_string = generate_eval_string(numbers, operators)

        # evaluate generated problem
        try:
            solution = eval(eval_string)
        except ZeroDivisionError:
            #print("Division by zero")
            continue

        # exit loop if valid problem was generated
        if is_valid_solution(solution):
            failed_generate = False
    
    return numbers, operators, eval_string, solution

def is_valid_solution(solution):
    if int(solution) != solution:
        return False
    if solution < 0:
        return False
    return True

def game_loop():
    play_again = True
    while play_again:
        numOperators = int(input("Enter a number of operators greater than zero: "))
        print("Generating problem with " + str(numOperators) + " operators...")
        numbers, operators, eval_string, solution = generate(numOperators)
        #print(eval_string)
        print(generate_eval_string(numbers, operators, "_"))
        print(solution)

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

if __name__ == "__main__":
    #game_loop()
    generate_prototype_problems()
    

# TODO: each operator has a priority (1, 2, 3, 4); USER sets the priority to place parentheses
# if priority is not set, order of operations will be followed