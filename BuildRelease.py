import os
import sys
from pathlib import Path
import shutil
import zipfile
from zipfile import ZipFile

import contextlib

def get_arg(arg, fallback):
	if len(sys.argv) > arg:
		val = sys.argv[arg]
		if val != None:
			return val
	return fallback

script_dir = Path(os.path.dirname(os.path.abspath(__file__)))
os.chdir(script_dir)

version = get_arg(1, None)

file_name = "BG3ModManager_v{}.zip".format(version)
print("Writing release zip:{}".format(file_name))

write_files = [
    "bin/Publish/_Lib",
    "bin/Publish/Resources",
    "bin/Publish/BG3ModManager.exe",
    "bin/Publish/BG3ModManager.exe.config",
]

def zipdir(src, zip_name):
    """
    Function creates zip archive from src in dst location. The name of archive is zip_name.
    :param src: Path to directory to be archived.
    :param dst: Path where archived dir will be stored.
    :param zip_name: The name of the archive.
    :return: None
    """
    ### zipfile handler
    ziph = zipfile.ZipFile(zip_name, 'w')
    ### writing content of src directory to the archive
    for root, dirs, files in os.walk(src):
        for file in files:
            ### In this case the structure of zip archive will be:
            ###       C:\Users\BO\Desktop\20200307.zip\Audacity\<content of Audacity dir>
            # ziph.write(os.path.join(root, file), arcname=os.path.join(root.replace(os.path.split(src)[0], ""), file))

            ### In this case the structure of zip archive will be:
            ###       C:\Users\BO\Desktop\20200307.zip\<content of Audacity dir>
            ziph.write(os.path.join(root, file), arcname=os.path.join(root.replace(src, ""), file))
    ziph.close()

def SilentRemove(f):
	try:
		if Path(f).is_dir():
			shutil.rmtree(f, ignore_errors=True)
		else:
			os.remove(f)
		print("Removed {}".format(f))
	except Exception as e:
		print(e)

def SilentCopyAndRemove(source, dest):
	with contextlib.suppress(FileNotFoundError, PermissionError):
		shutil.copy(source, dest)
		if Path(source).is_dir():
			shutil.rmtree(source, ignore_errors=True)
		else:
			os.remove(source)
		print("Removed {}".format(source))

import time
time.sleep(3)

SilentRemove("bin/Publish/Data")
SilentRemove("bin/Publish/_Logs")
SilentRemove(file_name)
SilentCopyAndRemove("bin/Publish/DivinityModManager.exe", "bin/Publish/BG3ModManager.exe")
SilentCopyAndRemove("bin/Publish/DivinityModManager.exe.config", "bin/Publish/BG3ModManager.exe.config")

zipdir("bin/Publish", file_name)

# writing files to a zipfile 
# with ZipFile(file_name,"w") as zip: 
# 	# writing each file one by one 
# 	for file in write_files:
# 		p = Path(file)
# 		print(str(p.absolute()), p.name)
# 		if p.is_file():
# 			zip.write(str(p.absolute()), p.name, compress_type = ZipFile.ZIP_DEFLATED)
# 		elif p.is_dir():
# 			for x in p.rglob(".*"):
# 				zip.write(str(x.absolute()), x.name, compress_type = ZipFile.ZIP_DEFLATED)