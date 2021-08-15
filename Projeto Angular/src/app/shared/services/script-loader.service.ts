import { Injectable } from '@angular/core';

declare var document: any;
interface Scripts {
  name: string;
  src: string;
}

interface Styles {
  name: string;
  href: string;
}

export const ScriptStore: Scripts[] = [
  { name: '', src: '' }
];

export const StyleStore: Styles[] = [
  { name: '', href: '' },
];

@Injectable()
export class ScriptLoaderService {

  private scripts: any = {};
  private styles: any = {};

  constructor() {
    this.initScriptStore();
    this.initStyleStore();
  }

  private initScriptStore() {
    ScriptStore.forEach((script: any) => {
      this.scripts[script.name] = {
        loaded: false,
        src: script.src
      };
    });
  }

  private initStyleStore() {
    StyleStore.forEach((style: any) => {
      this.styles[style.name] = {
        loaded: false,
        href: style.href
      };
    });
  }

  setScriptsToBeLoaded(...scripts: string[]) {
    const promises: any[] = [];
    scripts.forEach((script) => promises.push(this.scriptLoader(script)));
    return Promise.all(promises);
  }

  setStylesToBeLoaded(...styles: string[]) {
    const promises: any[] = [];
    styles.forEach((css) => promises.push(this.cssLoader(css)));
    return Promise.all(promises);
  }

  private scriptLoader(name: string) {
    return new Promise((resolve, reject) => {
      if (this.scripts[name].loaded) {
        resolve({ script: name, loaded: true, status: 'Already Loaded' });
      } else {
        const script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = this.scripts[name].src;
        if (script.readyState) {
          script.onreadystatechange = () => {
            if (script.readyState === 'loaded' || script.readyState === 'complete') {
              script.onreadystatechange = null;
              this.scripts[name].loaded = true;
              resolve({ script: name, loaded: true, status: 'Loaded' });
            }
          };
        } else {
          script.onload = () => {
            this.scripts[name].loaded = true;
            resolve({ script: name, loaded: true, status: 'Loaded' });
          };
        }
        script.onerror = (error: any) => resolve({ script: name, loaded: false, status: 'Loaded' });
        document.getElementsByTagName('body')[0].appendChild(script);
      }
    });
  }

  private cssLoader(cssName: string) {
    return new Promise((resolve, reject) => {
      if (this.styles[cssName].loaded) {
        resolve({ link: cssName, loaded: true, status: 'Already Loaded' });
      } else {
        const link = document.createElement('link');
        link.href = this.styles[cssName].href;
        link.type = 'text/css';
        link.rel = 'stylesheet';
        if (link.readyState) {
          link.onreadystatechange = () => {
            if (link.readyState === 'loaded' || link.readyState === 'complete') {
              link.onreadystatechange = null;
              this.styles[cssName].loaded = true;
              resolve({ link: cssName, loaded: true, status: 'Loaded' });
            }
          };
        } else {
          link.onload = () => {
            this.styles[cssName].loaded = true;
            resolve({ styles: cssName, loaded: true, status: 'Loaded' });
          };
        }
        link.onerror = (error: any) => resolve({ styles: cssName, loaded: false, status: 'Loaded' });
        document.getElementsByTagName('head')[0].appendChild(link);
      }
    });
  }
}
