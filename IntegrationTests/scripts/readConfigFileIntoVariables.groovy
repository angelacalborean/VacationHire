String configFile = props.get("configFile");
if(configFile==null || configFile==""){
 configFile="local.properties"
}
FileInputStream is = new FileInputStream(configFile);
Properties p = new Properties();
p.load(is);

p.entrySet().each {entry ->
    vars.putObject(entry.getKey(),entry.getValue())
}

is.close();