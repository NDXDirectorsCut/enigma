#include "Application.hpp"
#include <iostream>

bool Application::Start()
{
    platform.dataPath = executableName + "Data/";
    if(!platform.Init(applicationName))
    {
        return false;
    }
    
    std::cout<<"\n"+"Started " + applicationName + "\n";
    std::cout<<"Created by: " + creatorName + "\n";
    std::cout<<"Enigma Runtime version: " + runtimeVersion + "\n";
    std::cout<<"Data path: " + platform.dataPath + "\n";
    running = true;
    return true;
}

void Application::Update()
{

}

void Application::FixedUpdate()
{

}

Application::~Application()
{
    if(running)
        platform.Quit();
}