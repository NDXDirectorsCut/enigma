cmake_minimum_required(VERSION 3.15)
project(Enigma LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

set(PROJECT_PATH "" CACHE PATH "Project to build against Engima Runtime")



if(PROJECT_PATH)

    file(READ "${PROJECT_PATH}/ProjectInfo.ini" PROJECT_INFO)

    string(REGEX MATCH "ProjectName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    string(STRIP "${CMAKE_MATCH_1}" PROJECT_NAME)

    string(REGEX MATCH "ExecutableName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    string(STRIP "${CMAKE_MATCH_1}" EXECUTABLE_NAME)

    # string(REGEX MATCH "ApplicationName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    # string(STRIP "${CMAKE_MATCH_1}" APPLICATION_NAME)

    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME}/${EXECUTABLE_NAME}Data/Archive)
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME}/${EXECUTABLE_NAME}Data/Library)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME})

    # GLAD2
    add_library(glad STATIC
        External/glad/src/gl.c 
    )
    target_include_directories(glad PUBLIC External/glad/include)

    # SDL
    add_subdirectory(
        External/SDL
        ${CMAKE_BINARY_DIR}/External/SDL )

    file(GLOB_RECURSE RUNTIME_SRC
        Source/Runtime/*.cpp
        Source/Runtime/*.h
    )

    add_library(EnigmaRuntime SHARED ${RUNTIME_SRC})
    target_link_libraries(EnigmaRuntime PRIVATE SDL2 SDL2main glad)

    message(STATUS "Building ${PROJECT_NAME} with Enigma Runtime version -.-.- ")

    #file(GLOB_RECURSE PROJECT_SRC
    #    ${PROJECT_PATH}/*.cpp
    #    ${PROJECT_PATH}/*.h
    #)

    #add_library(ProjectAssembly Shared ${PROJECT_SRC})

    add_executable(${EXECUTABLE_NAME} Source/main.cpp)
    target_link_libraries(${EXECUTABLE_NAME} PRIVATE EnigmaRuntime)
    
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        PROJECT_NAME_STR="${PROJECT_NAME}"
        APPLICATION_NAME="${APPLICATION_NAME}"
    )

else()
    message(FATAL_ERROR "No Project to build!")
endif()