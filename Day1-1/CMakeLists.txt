cmake_minimum_required(VERSION 3.21)

set(CMAKE_C_STANDARD 23)
project(AdventOfCode-Day1-1 C)

add_executable(solution main.c)

set_target_properties(solution PROPERTIES RUNTIME_OUTPUT_DIRECTORY_DEBUG ${CMAKE_CURRENT_SOURCE_DIR})
