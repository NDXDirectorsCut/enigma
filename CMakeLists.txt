cmake_minimum_required(VERSION 3.10)
project(Enigma LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_SHARED_LIBRARY_PREFIX "")
set(CMAKE_STATIC_LIBRARY_PREFIX "")

set(PROJECT_PATH "" CACHE PATH "Enigma Project to build")

function(build_enigma_runtime)
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
    target_link_libraries(EnigmaRuntime PUBLIC SDL2 SDL2main glad)
    target_include_directories(EnigmaRuntime PUBLIC Source/Runtime )
endfunction()

if(PROJECT_PATH)
    #Get Project Info

    file(READ "${PROJECT_PATH}/ProjectInfo.ini" PROJECT_INFO)

    string(REGEX MATCH "ProjectName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    string(STRIP "${CMAKE_MATCH_1}" PROJECT_NAME)

    string(REGEX MATCH "ExecutableName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    string(STRIP "${CMAKE_MATCH_1}" EXECUTABLE_NAME)

    string(REGEX MATCH "ApplicationName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    string(STRIP "${CMAKE_MATCH_1}" APPLICATION_NAME)

    string(REGEX MATCH "CreatorName[ ]*=[ ]*\"?([^\"]*)\"?" _ "${PROJECT_INFO}")
    string(STRIP "${CMAKE_MATCH_1}" CREATOR_NAME)

    #Build Runtime/Dependencies
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME}/${EXECUTABLE_NAME}Data/Archive)
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME}/${EXECUTABLE_NAME}Data/Library)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME}/${EXECUTABLE_NAME}Data/Library)
    set(CMAKE_BUILD_RPATH "$ORIGIN")

    build_enigma_runtime()

    #Build Project

    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PROJECT_NAME})
    message(STATUS "Building ${PROJECT_NAME}")

    add_executable(${EXECUTABLE_NAME} Source/main.cpp)
    target_link_libraries(${EXECUTABLE_NAME} PRIVATE EnigmaRuntime)
    
    set_target_properties(${EXECUTABLE_NAME} PROPERTIES 
        BUILD_WITH_INSTALL_RPATH TRUE
        INSTALL_RPATH "$ORIGIN/${EXECUTABLE_NAME}Data/Library" 
    )

    target_compile_definitions(${EXECUTABLE_NAME} PRIVATE
        EXECUTABLE_NAME="${EXECUTABLE_NAME}"
        PROJECT_NAME="${PROJECT_NAME}"
        APPLICATION_NAME="${APPLICATION_NAME}"
        CREATOR_NAME="${CREATOR_NAME}"
    )

else()
    message(STATUS "No Project given, building Runtime Only instead")
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/Runtime/Archive)
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/Runtime/Library)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/Runtime/Library)

    build_enigma_runtime()

endif()