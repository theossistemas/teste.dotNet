export var adminLteConf = {
  skin: 'blue',  
  sidebarLeftMenu: [
    // {label: 'MAIN NAVIGATION', separator: true},
    {label: 'Home', route: 'dashboard', iconClasses: 'fa fa-th'},
    {label: 'Livro', route: 'cadastro-livro', iconClasses: 'fa fa-book'},
    {label: 'Autor', route: 'arquivos', iconClasses: 'fa fa-user-o'},
    {label: 'Genero', route: 'financeiro', iconClasses: 'fa fa-pencil'},
    // {label: 'Relatórios', route: 'relatorio', iconClasses: 'fa fa-bar-chart'},
    // {label: 'Parent', iconClasses: 'fa fa-files-o', children: [
    //   {label: 'Children', route: 'parent/children'},
    //   {label: 'Parent 2', children: [
    //     {label: 'Children 2', route: 'parent/parent2/children2'}
    //   ]}      
    // ]}
  ]
};

/*
export const adminLteConf = {
  skin: 'blue',
  // isSidebarLeftCollapsed: false,
  // isSidebarLeftExpandOnOver: false,
  // isSidebarLeftMouseOver: false,
  // isSidebarLeftMini: true,
  // sidebarRightSkin: 'dark',
  // isSidebarRightCollapsed: true,
  // isSidebarRightOverContent: true,
  // layout: 'normal',
  sidebarLeftMenu: [
    {label: 'MAIN NAVIGATION', separator: true},
    {label: 'Get Started', route: '/', iconClasses: 'fa fa-road', pullRights: [{text: 'New', classes: 'label pull-right bg-green'}]},
    {label: 'Layout', iconClasses: 'fa fa-th-list', children: [
        {label: 'Configuration', route: 'layout/configuration'},
        {label: 'Custom', route: 'layout/custom'},
        {label: 'Header', route: 'layout/header'},
        {label: 'Sidebar Left', route: 'layout/sidebar-left'},
        {label: 'Sidebar Right', route: 'layout/sidebar-right'},
        {label: 'Content', route: 'layout/content'}
      ]},
    {label: 'COMPONENTS', separator: true},
    {label: 'Accordion', route: 'accordion', iconClasses: 'fa fa-tasks'},
    {label: 'Alert', route: 'alert', iconClasses: 'fa fa-exclamation-triangle'},
    {label: 'Boxs', iconClasses: 'fa fa-files-o', children: [
        {label: 'Default Box', route: 'boxs/box'},
        {label: 'Info Box', route: 'boxs/info-box'},
        {label: 'Small Box', route: 'boxs/small-box'}
      ]},
    {label: 'Dropdown', route: 'dropdown', iconClasses: 'fa fa-arrows-v'},
    {label: 'Form', iconClasses: 'fa fa-files-o', children: [
        {label: 'Input Text', route: 'form/input-text'}
    ]},
    {label: 'Tabs', route: 'tabs', iconClasses: 'fa fa-th'}
  ]
};

*/