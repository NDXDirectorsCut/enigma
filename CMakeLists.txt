cmake_minimum_required(VERSION 3.50)
project(Enigma LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

include_directories(
    External/glad/include
)

# GLAD2
add_library(glad STATIC
    External/glad/src/gl.c 
)
target_include_directories(glad PUBLIC External/glad/include)

# SDL
add_subdirectory(External/SDL)


file(GLOB_RECURSE EDITOR_SRC
    Source/Editor/*.cpp
    Source/Editor/*.h
)

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

add_executable(EnigmaEditor ${EDITOR_SRC})

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

target_link_libraries(EnigmaEditor PRIVATE glad SDL2 SDL2main imgui)

if(APPLE)
    target_link_libraries(EnigmaEditor PRIVATE "-framework OpenGL")
elseif(UNIX)
    find_package(OpenGL REQUIRED)
    target_link_libraries(EnigmaEditor PRIVATE OpenGL::GL dl pthread)
endif()