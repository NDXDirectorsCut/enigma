cmake_minimum_required(VERSION 3.15)
project(Enigma LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

set(GAME_PROJECT_PATH "" CACHE PATH "Optional game project path.")

include_directories(
    External/glad/include
    External/SDL
)

# GLAD2
add_library(glad STATIC
    External/glad/src/gl.c 
)
target_include_directories(glad PUBLIC /Library)

# SDL
add_subdirectory(External/SDL)

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin/Archive)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin/Library)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin/)

# Dear ImGUI
file(GLOB IMGUI_SRC
    External/imgui/*.cpp
    External/imgui/backends/imgui_impl_sdl.cpp
    External/imgui/backends/imgui_impl_opengl3.cpp
)

add_library(imgui STATIC ${IMGUI_SRC})

target_include_directories(imgui PUBLIC
    External/imgui
    External/imgui/backends
)

if(GAME_PROJECT_PATH)
    message(STATUS "Building game project: ${GAME_PROJECT_PATH}")

    file(GLOB_RECURSE PROJECT_SRC
        ${GAME_PROJECT_PATH}/*.cpp
        ${GAME_PROJECT_PATH}/*.h
    )

    file(GLOB_RECURSE RUNTIME_SRC
        Source/Runtime/*.cpp
        Source/Runtime/*.h
    )

    add_executable(Game ${RUNTIME_SRC} ${PROJECT_SRC})
    target_link_libraries(EnigmaGame PRIVATE SDL2 SDL2main glad imgui)

else()
    message(STATUS "Building Enigma Editor")

    file(GLOB_RECURSE EDITOR_SRC
        Source/Runtime/*.cpp
        Source/Runtime/*.h
        Source/Editor/*.cpp
        Source/Editor/*.h
    )

    add_executable(EnigmaEditor ${EDITOR_SRC})
    target_link_libraries(EnigmaEditor PRIVATE SDL2 SDL2main glad imgui)
endif()

if(APPLE)
    target_link_libraries(Enigma PRIVATE "-framework OpenGL")
elseif(UNIX)
    find_package(OpenGL REQUIRED)
    target_link_libraries(Enigma PRIVATE OpenGL::GL dl pthread)
endif()