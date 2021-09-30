###############################################################################
### Makefile_TAG ############################### SCRIPT_TAG (210930:18h:54) ###
###############################################################################
:cd     $APROJECTS/GITHUB                       |:e Makefile
:cd     $APROJECTS/GITLAB                       |:e Makefile
:cd     $APROJECTS/iwintoo                      |:e Makefile
:cd     $WPROJECTS/RTabs/Util/RTabs_Profiles/DEV|:e Makefile
### ✔✔✔ C:/LOCAL/STORE/DEV/HARDWARE/DX1Utility  |:e Makefile

###############################################################################
# MAKE {{{
 TOP		= $(LOCAL)/STORE/DEV/HARDWARE
 APPDIR		= DX1Utility
 ARCH_DIR       = ../ARCHIVES_DX1
 LS		= ls.exe
 JAR		= jar.exe
#}}}

:cd %:h|update|only|set columns=999|vert terminal make arch
#arch {{{
 arch:
	@(echo "[$@]";\
	   FILES="Profiles/Global.xml `find .  -type f | egrep -v '\<(Profiles|txt|log|html|vim|tags|BAK|_gsdata_|ini|lnk|vs|obj|git|suo|exe|dll|pdb|application|lnk)\>' | sed -e 's/^\.\/*//' -e 's/ /\n/'`";\
	   LINKS=`find R* -type l | sed -e 's/^\.\/*//' -e 's/ /\n/'`;\
	\
	LINKS=`echo "$$LINKS" | grep -v '.cs'`;\
	LINKS_COUNT=`echo "$$LINKS" | wc -l`;\
	echo "$$LINKS_COUNT LINK files";\
	echo "$$LINKS";\
	\
	FILES_MISC=`echo "$$FILES" | grep -v '.cs'`;\
	FILES_MISC_COUNT=`echo "$$FILES_MISC" | wc -l`;\
	echo "$$FILES_MISC_COUNT MISC files";\
	echo "$$FILES_MISC";\
	\
	FILES_CS=`echo "$$FILES" | grep    '.cs'`;\
	FILES_CS_COUNT=`echo "$$FILES_CS" | wc -l`;\
	echo "$$FILES_CS_COUNT cs files";\
	echo "$$FILES_CS";\
	\
	echo "ARCHIVE";\
	    DATE=`date '+%y%m%d'`;\
	    ARCHIVE="$(APPDIR)_$$DATE.zip";\
	    rm -f                    $(ARCH_DIR)/$$ARCHIVE;\
	    $(JAR)   Mcf             $(ARCH_DIR)/$$ARCHIVE $$FILES;\
	    $(LS)         -lt        $(ARCH_DIR)/*.zip | head -2;\
	    $(JAR)   tvf             $(ARCH_DIR)/$$ARCHIVE;\
	\
	echo "[$@ DONE]";\
	)

#}}}

:cd %:h|update|only|set columns=999|vert terminal make GITHUB
#GITHUB {{{
 GITHUB:
	@(echo "[$@]";\
	cp -r DX1*.bat DX1*.exe Graphics Makefile PROJECT Profiles ../GITHUB_DX1Utility;\
	\
	rm -rf ../GITHUB_DX1Utility/PROJECT/.vs/;\
	rm -rf ../GITHUB_DX1Utility/PROJECT/obj/;\
	rm -rf ../GITHUB_DX1Utility/Profiles/BAK/;\
	rm -rf ../GITHUB_DX1Utility/PROJECT/bin/;\
	rm -rf ../GITHUB_DX1Utility/Profiles/Macros/;\
	rm -f  ../GITHUB_DX1Utility/Graphics/C_PROGRAMS_UTIL_ICONES_LOGOS.lnk;\
	rm -f  ../GITHUB_DX1Utility/PROJECT/ftp_remotetabs_PUT.txt;\
	rm -f  ../GITHUB_DX1Utility/PROJECT/index.html;\
	rm -f  ../GITHUB_DX1Utility/Profiles/DX1Utility.log;\
	rm -f  ../GITHUB_DX1Utility/Profiles/DX1Utility_UNATTENDED.log;\
	rm -f  ../GITHUB_DX1Utility/Profiles/Dx1Profiles.dat;\
	rm -f  ../GITHUB_DX1Utility/Profiles/Global.dx1_profile;\
	rm -f  ../GITHUB_DX1Utility/Profiles/Global.pgm;\
	rm -f  ../GITHUB_DX1Utility/Profiles/IWE.xml;\
	rm -f  ../GITHUB_DX1Utility/Profiles/NNN.xml;\
	rm -f  ../GITHUB_DX1Utility/Profiles/TMP.xml;\
	rm -f  ../GITHUB_DX1Utility/Profiles/TMP2.xml;\
	rm -f  ../GITHUB_DX1Utility/Profiles/ppp.xml;\
	\
	echo "[git status -s]";\
	cd     ../GITHUB_DX1Utility; git status -s;\
	\
	echo "[$@ DONE]";\
	)
#}}}

:only|vert terminal sh -c "cd %:h/../GITHUB_DX1Utility"
:only|vert terminal sh -c "cd %:h/../GITHUB_DX1Utility; git status -s"
:only|vert terminal sh -c "cd %:h/../GITHUB_DX1Utility; git status"
:!start explorer .

# vim: noexpandtab ts=8 sw=8
