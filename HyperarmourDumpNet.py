#!/usr/bin/python3

import clr
clr.AddReference("lib/SoulsFormats")
#clr.AddReference("lib/oo2core_6_win64")

from SoulsFormats import *

resourcesPath = "resources/1.07"
# BND4 file
anibndPath = resourcesPath + "/c0000.anibnd.dcx"
regulationPath = resourcesPath + "/regulation.bin"
# with open(anibndPath, 'rb') as f:
#     anibndBytes = f.read()

br = BND4Reader(anibndPath)



#x = bnd.Read(anibndPath)


a=1