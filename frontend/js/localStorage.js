// Author: Marcelo Guimarães da Costa
// Last update: 2020-03-22

//references:
//https://blog.logrocket.com/the-complete-guide-to-using-localstorage-in-javascript-apps-ba44edb53a36/
//https://pt.stackoverflow.com/questions/75557/passando-valores-js-para-outra-pagina-html

function ls_save(name, value){
    console.log(
        "Saving var at LocalStorage:\n"
        + "Name: "+name+"\n"
        + "Value: "+JSON.stringify(value)
    );
    
    localStorage.setItem(name, JSON.stringify(value));
}

function ls_retrieve(name){
    
    var value = JSON.parse(localStorage.getItem(name));

    console.log(
        "Retrieving var from LocalStorage:\n"
        + "Name: "+name+"\n"
        + "Value: "+JSON.stringify(value)
    );
    
    return value;
}

function ls_clear(){ 
    console.log("Cleaning LocalStorage");

    localStorage.clear();
}

function ls_isset(name){
    var isset = false;

    var value = localStorage.getItem(name)

    isset = value != null && value != undefined;

    console.log("Checking if LocalStorage var is set"
    + "Name: "+name+"\n"
    + "Is set: "+ isset);

    return isset;
}