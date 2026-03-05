#include <iostream>
#include "Application/Application.hpp"

int n;

int main(int argc, char* argv[])
{ 
    Application application;
    if(!application.Start())
    {
        std::cout<<"Initialization Failed! \n";
        return 1;
    }

    while(application.IsRunning())
    {
        application.Update();
        application.FixedUpdate();
    }

    return 0;
}